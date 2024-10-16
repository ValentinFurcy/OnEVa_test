pipeline {
    agent any

    environment {
        BUILD_PATH = 'output' // Répertoire où les artefacts sont générés
        SONARQUBE_SERVER = 'MySonarQubeServer' // Nom du serveur défini dans la configuration Jenkins
    }
    stages {
        stage('Checkout'){
            steps{
                git branch: 'main', credentialsId: '4e979ea0-06ab-4b0a-ac8e-a20f03916f29', url: 'https://github.com/ValentinFurcy/OnEVa_test.git'        
            }
        }
        stage('Restore Dependencies') {
            steps {
                sh "ls"
                sh 'cd OnEVa_API'
                sh 'echo $PWD'
                sh 'dotnet restore OnEVa_API'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build OnEVa_API --configuration Release'
            }
        }
        // stage('Run Tests') {
        //     steps {
        //         script {
        //             // Run unit tests and check status
        //             def testResult = sh(script: 'dotnet test OnEVa_API --logger:trx', returnStatus: true)
        //             if (testResult != 0) {
        //                 error("Unit tests failed. Please fix the issues before merging.")
        //             } else {
        //                 echo "Unit tests passed."
        //             }
        //         }
        //     }
        // }
        stage('Publish') {
            steps {
                sh "dotnet publish OnEVa_API --configuration Release -o ${BUILD_PATH}"
                // sh "dotnet publish OnEva_API/OnEVa_API.csproj --configuration Release -o ${BUILD_PATH}"
            }
        }
        stage('SonarQube Analysis') {
            steps {
                script {
                    echo 'Début de l\'analyse SonarQube'
                    def scannerHome = tool name: 'SonarQube', type: 'hudson.plugins.sonar.MsBuildSQRunnerInstallation'

                    // Récupérer le token depuis les credentials Jenkins
                    echo 'Récupération du token dans les credentials'
                    withCredentials([string(credentialsId: 'SonarQube', variable: 'SONAR_TOKEN')]) {
                    withSonarQubeEnv(SONARQUBE_SERVER) {
                        echo "Starting SonarQube analysis..."
                        // Étape "begin" pour démarrer l'analyse
                        sh "dotnet OnEVa_API ${scannerHome}/SonarScanner.MSBuild.dll begin /k:'JenkinsDemo' /d:sonar.login=${SONAR_TOKEN} /d:sonar.host.url=${SONARQUBE_URL}"
                    
                        // Build du projet (obligatoire après "begin")
                        echo "build projet"
                        sh 'dotnet build OnEVa_API --configuration Release'
                    
                        // Étape "end" pour terminer l'analyse
                        echo "End"
                        sh "dotnet OnEVa_API ${scannerHome}/SonarScanner.MSBuild.dll end /d:sonar.login=${SONAR_TOKEN}"
                    
                        echo 'Analyse terminée'
                }
                echo 'Fin script'
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
