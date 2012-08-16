class Project
  attr_accessor :Name, :Description, :Author, :Version, :FilePath, :Platforms, :Dependencies

  def initialize(path, version)
	@FilePath = path
	@Version = version
	@Name = @FilePath.split(/\//).last.split(/.csproj/).first
	@Description = "Deville Solutions Application Block"
	@Author = "Deville Solutions Inc."
	@Platforms = ["NET40", "SL40", "SL3-WP", "winrt45", ".NETPortable40-Profile2"]
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