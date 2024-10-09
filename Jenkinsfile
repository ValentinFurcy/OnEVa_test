pipeline {
    agent any

    environment {
        BUILD_PATH = 'output' // Répertoire où les artefacts sont générés
        SONARQUBE_SERVER = 'SonarQube' // Nom du serveur défini dans la configuration Jenkins
    }
    stages {
        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
        stage('Run Tests') {
            steps {
                script {
                    // Run unit tests and check status
                    def testResult = sh(script: 'dotnet test --logger:trx', returnStatus: true)
                    if (testResult != 0) {
                        error("Unit tests failed. Please fix the issues before merging.")
                    } else {
                        echo "Unit tests passed."
                    }
                }
            }
        }
        stage('Publish') {
            steps {
                sh "dotnet publish src/LibraryManagement/LibraryManagement.csproj --configuration Release -o ${BUILD_PATH}"
            }
        }
        stage('SonarQube Analysis') {
            steps {
                script {
                    def scannerHome = "/opt/sonar-scanner/SonarScanner.MSBuild.dll"
                    def scannerHome = tool name: 'SonarScanner for MSBuild 9.0.0', type: 'hudson.plugins.sonar.MsBuildSQRunnerInstallation'

                    Récupérer le token depuis les credentials Jenkins
                    withCredentials([string(credentialsId: '78cccfbd-7fe9-4046-b77c-cb7973f3b0b7', variable: 'SONAR_TOKEN')]) {
                        withSonarQubeEnv(SonarQube) {
                            //Étape "begin" pour démarrer l'analyse
                            sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:'JenkinsDemo' /d:sonar.login=${SONAR_TOKEN} /d:sonar.host.url=${SONARQUBE_URL}"
                            //Build du projet (obligatoire après "begin")
                            sh 'dotnet build --configuration Release'
                            //Étape "end" pour terminer l'analyse
                            sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll end /d:sonar.login=${SONAR_TOKEN}"
                        }
                    }
                }
            }
        
        stage('Archive Artifact') {
            steps {
                archiveArtifacts artifacts: "${BUILD_PATH}/**", allowEmptyArchive: false
            }
        }
    }
}
