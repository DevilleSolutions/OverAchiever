namespace :build do

	task :all, :config do |task, args|
		FileList["**/*.sln"].each do |solution|
			builder = Rake::Task["build:solution"]
			builder.invoke(solution, args.config || :Debug)
			builder.reenable
		end
	end
	
	desc "Build the project"
	msbuild :solution, [:solution, :config] do |msb, args|
		msb.properties :configuration => args.config || :Debug
		msb.targets :Build
		msb.solution = args.solution
	end

	desc "Rebuild the project"
	task :re => ["clean", "build:all"]
end