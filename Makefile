#make update-version VERSION=7.6.1.
.PHONY: update-version

update-version:
	@if [ -z "${VERSION}" ]; then \
		echo "VERSION is not set. Set VERSION as an environment variable."; \
		exit 1;\
	fi
	chmod +x ./scripts/update-master.sh
	chmod +x ./scripts/update-version-android.sh
#	chmod +x ./scripts/update-version-ios.sh
	./scripts/update-version-android.sh ${VERSION}
#	./scripts/update-version-ios.sh ${VERSION}

