require 'rubygems'    

puts "Chequing bundled dependencies, please wait...."

system "bundle install --system --quiet"
Gem.clear_paths

require 'albacore'
require 'git'
require 'rake/clean'

include FileUtils

# Import additional tasks
Dir.glob('tasks/*.rake').each { |r| import r }

class Project
  attr_accessor :Name, :Description, :Author, :Version, :FilePath, :Platforms, :Dependencies

  def initialize(path, version)
	@FilePath = path
	@Version = version
	@Name = @FilePath.split(/\//).last.split(/.csproj/).first
	@Description = "Deville Solutions Application Block"
	@Author = "Deville Solutions Inc."
	@Platforms = ["NET40"]
	@Dependencies = Array.new

	LoadPackageDependencies()
	LoadProjectDependencies()
  end

  def LoadPackageDependencies()
	path = "#{File::dirname @FilePath}/packages.config"

	if File.exists? path
		packageConfigXml = File.read("#{File::dirname project.FilePath}/packages.config")
		doc = REXML::Document.new(packageConfigXml)
		doc.elements.each("packages/package") do |package|
			@Dependencies << Dependency.new(package.attributes["id"], package.attributes["version"])
		end
	end
  end

  def LoadProjectDependencies()
	if File.exists? @FilePath
		projectFileXml = File.read(@FilePath)
		doc = REXML::Document.new(projectFileXml)
		doc.elements.each("Project/ItemGroup/ProjectReference/Name") do |proj|
			@Dependencies << Dependency.new(proj.text, @Version)
		end
	end
  end
end

class Dependency
	attr_accessor :Name, :Version

	def initialize(name, version)
		@Name = name
		@Version = version
	end
end

solution_file = FileList["*.sln"].first
build_file = FileList["*.msbuild"].first
commit = Git.open(".").log.first.sha[0..10] rescue 'na'
release_notes = `git log --format="%d%n     %h - %s"`

CLEAN.include("main/**/bin", "main/**/obj", "*.xml", "*.gemspec", "*.vsmdi", "test/**/obj", "test/**/bin", "*.testsettings")

CLOBBER.include("**/_*", "**/.svn", "Packages/*", "**/*.user", "**/*.cache", "**/*.suo")

version_string = IO.readlines('VERSION')[0] rescue "0.0.0"
commit_number = `git describe --long`.split(/-/)[1] rescue "0"
version = "#{version_string}.#{commit_number}"

projects = Array.new

FileList["**/*.csproj"].each do |projectFile|
	projects << Project.new(projectFile, version)
end

task :default => ["nuget:restore"] do
	Rake::Task["utils:assemblyInfo"].invoke projects
	Rake::Task["build:all"].invoke
end

task :publish, :beta do |t, args|
	Rake::Task["nuget:restore"].invoke
	Rake::Task["build:all"].invoke
	projects = GetProjects(version)
	`rm -rf "publish"` if File.directory? "publish"

	projects.each do |project|
		puts "Project version #{project.Version}"
		assemblyInfo = Rake::Task["utils:assemblyInfo"]
		assemblyInfo.invoke project
		assemblyInfo.reenable

		publish_dir = "publish/#{project.Name}"
		project.Platforms.each do |platform|
			dir = "#{publish_dir}/lib/#{platform}"
			`mkdir "#{dir}"`
			cp FileList["**/*bin/release/#{project.Name}.dll"].first, dir
		end

		spec = Rake::Task["nuget:spec"]
		spec.invoke project, publish_dir, args.beta
		spec.reenable
	end

	FileList["publish/**/*.nuspec"].each do |spec|
		pack = Rake::Task["nuget:pack"]
		pack.invoke(spec)
		pack.reenable
	end

	FileList["publish/**/*.nupkg"].each do |pkg|
		publish = Rake::Task["nuget:publish"]
		publish.invoke(pkg, "http://www.myget.org/F/evildev/")
		publish.reenable
	end
end