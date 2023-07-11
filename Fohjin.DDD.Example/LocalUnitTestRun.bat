
REM @REM SET
@echo off

SETLOCAL

SET TestProject=Fohjin.DDD.sln
SET Configuration=Release

IF NOT "%1"=="" IF NOT "%TestFilter%"=="" SET TestFilter=

:check_again
IF "%1"=="" GOTO ready_to_run

SET temp_filter=%1
IF "%TestFilter%"=="" (
	SET TestFilter=TestCategory=%temp_filter%
) ELSE (
	SET TestFilter=%TestFilter%^|TestCategory=%temp_filter%
)

SHIFT
GOTO check_again

:ready_to_run
IF "%TestFilter%"=="" SET TestFilter=TestCategory=Unit^|TestCategory=Simulate

ECHO "%TestFilter%"

dotnet tool install --global dotnet-reportgenerator-globaltool 2>NUL
rmdir /s/q ".\TestResults"
mkdir ".\TestResults"

dotnet test "%TestProject%" --configuration %Configuration% --results-directory .\TestResults --nologo --settings .runsettings
REM --filter "%TestFilter%"

SET TEST_ERR=%ERRORLEVEL%

reportgenerator "-reports:.\TestResults\**\coverage.cobertura.xml" "-targetDir:.\TestResults\Coverage\Reports" -reportTypes:HtmlSummary;Cobertura "-title:%TestProject% - (%USERNAME%)"


start .\TestResults\Coverage\Reports\summary.html

ECHO TEST_ERR=%TEST_ERR%
IF "%TEST_ERR%"=="0" (
	ECHO "No Errors :)"
)
ENDLOCAL

