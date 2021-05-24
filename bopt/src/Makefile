VERSION=1

dev:
	@make buildProj
	@make run

run:
	@../build/bopt_${VERSION}
buildProj:
	@echo bulding project
	@go build -o ../build/bopt_${VERSION}


prod:
	@echo TODO: setup a production build command
clean:
	rm build/bopt_${VERSION}
