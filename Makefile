#make update-version VERSION=7.6.1.
.PHONY: setup update-version

setup:
	chmod +x ./scripts/update-master.sh
	chmod +x ./scripts/update-version-android.sh
	chmod +x ./scripts/update-version-ios.sh

update-version:
	@if [ -z "${VERSION}" ]; then \
		echo "VERSION is not set. Set VERSION as an environment variable."; \
		echo "make update-version VERSION=7.6.1"; \
		exit 1;\
	fi
	./scripts/update-version-android.sh ${VERSION}
#	./scripts/update-version-ios.sh ${VERSION}

