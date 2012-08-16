namespace :nuget do
	desc "Setup dependencies for nuget packages"
	task :restore, :output_dir do |task, args|
		output = args.output_dir || "Packages"
		`mkdir "#{output}"` unless File.directory? output

		FileList["**/packages.config"].each do |file|
			`#{FileList["**/nuget.exe"].first} install "#{file}" /OutputDirectory "#{output}"`
		end
	end

	desc "Generates a package specification for a project"
	nuspec :spec, [:project, :working_dir, :beta]  do |nuspec, args|
		project = args.project
		nuspec.id = project.Name
		nuspec.description = project.Description
		nuspec.version = project.Version
		nuspec.authors = project.Author
		nuspec.owners = project.Author
		nuspec.language = "en-US"
		project.Dependencies.each do |dep|
			nuspec.dependency dep.Name, dep.Version
		end
		nuspec.licenseUrl = "http://www.gnu.org/licenses/lgpl.txt"
		nuspec.working_directory = args.working_dir
		nuspec.output_file = "#{project.Name}.#{project.Version}#{args.beta}.nuspec"
		nuspec.tags = ""
	end

	desc "Generates a nuget package for the specified spec"
	nugetpack :pack, :spec do |p, args|
		p.command = FileList["**/nuget.exe"].first
		p.nuspec = args.spec
		p.output = "publish"
	end

	nugetpush :publish, [:package, :source] do |nuget, args|
		nuget.command = FileList["**/nuget.exe"].first
		nuget.package = "./#{args.package}"
		nuget.apikey = "fdbc4aa1-9394-4e39-9207-4b2fad6efc84"
		if args.source?
			nuget.source = "#{args.source}"
		end
	end
end