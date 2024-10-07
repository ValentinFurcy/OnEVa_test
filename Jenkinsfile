pipeline {
    agent any

    tools {
        // Utilisation du SDK .NET 8.0 et de NuGet
        dotnetsdk 'sdk.NET8.0.8'
        msbuild 'MSBuild2022'
    }

    stages {
        stage('Checkout') {
            steps {
                // Récupérer le code depuis GitHub
                git 'https://github.com/yourusername/your-dotnet-project'
            }
        }
        
        stage('Restore NuGet Packages') {
            steps {
                // Restaurer les paquets NuGet
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                // Compiler le projet avec MSBuild
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Run Tests') {
            steps {
                // Exécuter les tests unitaires
                sh 'dotnet test --configuration Release --no-build'
            }
        }

        stage('SonarQube Analysis (dotnet)') {
            steps {
                // Analyse SonarQube pour un projet .NET
                withSonarQubeEnv('SonarQ') {
                // Utilisation des credentials Jenkins pour le token SonarQube
                withCredentials([string(credentialsId: '0f4aa489-7b24-4e40-bde1-92b67d7fbbda', variable: 'SONAR_TOKEN')]) {
                
                    // Démarrage de l'analyse SonarQube pour le projet .NET
                    sh 'dotnet sonarscanner begin /k:"OnEVa_test" /d:sonar.login=$SONAR_TOKEN'

                    // Compilation du projet .NET
                    sh 'dotnet build'

                    // Terminer l'analyse SonarQube pour .NET
                    sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN'
                    }
                }
            }
        }
    }
    
    post {
        success {
            // Publish test results to GitHub
            junit 'TestResults/*.trx'
            // Notify GitHub of successful verification
            echo 'Notifying GitHub of successful build...'
            script {
               // Message to send
                def message = "The build was successful!"

                // Using the 'httpRequest' Jenkins plugin to send the request
                def response = httpRequest (
                    url: githubApiUrl,
                    httpMode: 'POST',
                    contentType: 'APPLICATION_JSON',
                    requestBody: "{\"body\": \"$message\"}",
                    authentication: 'GITHUB_TOKEN'  // Nom de l'authentification configuré dans Jenkins
            )

            // Log the response
            echo "Response: ${response.status}"
            }
        }
        failure {
            // Notify GitHub that verification has failed
            echo 'Notifying GitHub of failed build...'
        }
    }
}
