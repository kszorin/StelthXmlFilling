@echo lpmaker-wrapper v2.0

@set LPMAKER_SILENT=no
@if '%1'=='--silent' set LPMAKER_SILENT=yes
@if %LPMAKER_SILENT%==yes shift

@set LPMAKER_CERT=
@if '%1'=='--cert' goto cert
@goto main

:cert
@shift
@if '%1'=='' goto cert_required
@set LPMAKER_CERT=%1
shift

:main
@set LPMAKER_INPUT=%1
@set LPMAKER_CONTAINER=%2
@set LPMAKER_LICENSES=%3

@if '%LPMAKER_INPUT%'==''     goto usage
@if '%LPMAKER_CONTAINER%'=='' goto usage

@set LPMAKER_TEMP_INPUT=temp-licenses-pack.xml
@set LPMAKER_TEMP_CONTAINER=temp-licenses-container.bin
@set LPMAKER_TEMP_LICENSES=temp-licenses-file.bin

copy %LPMAKER_INPUT% %LPMAKER_TEMP_INPUT%
@if %LPMAKER_SILENT%==yes goto silent_run
@if '%LPMAKER_CERT%'=='' goto run_no_cert
lpmaker.exe --cert %LPMAKER_CERT% %LPMAKER_TEMP_INPUT% %LPMAKER_TEMP_CONTAINER% %LPMAKER_TEMP_LICENSES%
@goto make_copies
:run_no_cert
lpmaker.exe %LPMAKER_TEMP_INPUT% %LPMAKER_TEMP_CONTAINER% %LPMAKER_TEMP_LICENSES% > a.out 2> a.err
@goto make_copies

:silent_run
@if '%LPMAKER_CERT%'=='' goto silent_run_no_cert
lpmaker.exe --silent --cert %LPMAKER_CERT% %LPMAKER_TEMP_INPUT% %LPMAKER_TEMP_CONTAINER% %LPMAKER_TEMP_LICENSES%
@goto make_copies
:silent_run_no_cert
lpmaker.exe --silent %LPMAKER_TEMP_INPUT% %LPMAKER_TEMP_CONTAINER% %LPMAKER_TEMP_LICENSES% > a.out 2> a.err
@goto make_copies

:make_copies
copy %LPMAKER_TEMP_CONTAINER% %LPMAKER_CONTAINER%
@if '%LPMAKER_LICENSES%'=='' goto make_default_licenses
copy %LPMAKER_TEMP_LICENSES% %LPMAKER_LICENSES%
@goto cleanup

:make_default_licenses
copy %LPMAKER_TEMP_LICENSES% licenses.bin

@goto cleanup

:cleanup
del %LPMAKER_TEMP_INPUT% 
del %LPMAKER_TEMP_CONTAINER% 
del %LPMAKER_TEMP_LICENSES%
@set LPMAKER_SILENT=
@set LPMAKER_INPUT=
@set LPMAKER_CONTAINER=
@set LPMAKER_LICENSES=
@set LPMAKER_TEMP_INPUT=
@set LPMAKER_TEMP_CONTAINER=
@set LPMAKER_TEMP_LICENSES=
@set LPMAKER_CERT=
@goto exit

:cert_required
@echo Certificate file not specified.
@goto usage

:usage
@echo Usage: lpmaker-wrapper [--silent] [--cert certificate-file] input-xml-file container-file [licenses-file]
@goto exit

:exit