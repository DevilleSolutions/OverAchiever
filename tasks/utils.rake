namespace :utils do
	task :clean_folder, :folder do |t, args|
		rm_rf(args.folder)
		Dir.mkdir(args.folder) unless File.directory? args.folder
	end

	task :assemblyInfo, :projects do |task, args|
		projTask = Rake::Task["utils:projectMetaData"]

		args.projects.each do |project|
			projTask.invoke project
			projTask.reenable
		end
	end

	assemblyinfo :projectMetaData, :project do |asm, args|
		project = args.project
		project_dir = File::dirname project.FilePath

		asm.version = project.Version
		asm.company_name = project.Author
		asm.product_name = "#{project.Name}"
		asm.copyright = "#{project.Author} #{DateTime.now.year}"
		asm.output_file = "#{project_dir}/Properties/AssemblyInfo.cs"
	end

	task :build_release => [:update_version] do 
		Rake::Task["build:all"].invoke(:Release)
	end
end