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

        stage('SonarQube Analysis') {
            steps {
                script {
                    withSonarQubeEnv('SonarQ') {
                        sh "${mvnHome}/bin/mvn clean verify sonar:sonar -Dsonar.projectKey=Vulnado -Dsonar.projectName='Vulnado'"
                        // Analyse SonarQube
                        sh 'dotnet sonarscanner begin /k:"your_project_key" /d:sonar.login=$SONAR_TOKEN'
                        sh 'dotnet build'
                        sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN'
                    }
                }
            }
        }

        stage('OWASP Dependency-Check') {
            steps {
                dependencyCheck additionalArguments: '--project "Your Project" --format "ALL" --scan "./"', odcInstallation: 'Default'
            }
        }

        stage('Snyk Security Check') {
            steps {
                snykSecurity failOnIssues: true, snykInstallation: 'Default'
            }
        }
    }

    post {
        always {
            // Archiver les rapports de build et les résultats des tests
            archiveArtifacts artifacts: '**/bin/**/*.dll', allowEmptyArchive: true
            junit '**/TestResults/*.xml'
        }
    }
}
