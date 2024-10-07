pipeline {
  agent any

  tools {
    dotnetsdk 'sdk.NET8.0.8'  // Name of your .NET SDK installation configured in Jenkins
  }

  stages {
    stage('Clean Workspace') {
      steps {
        cleanWs()
      }
    }

    stage('Checkout') {
      steps {
        git branch: 'main', url: 'https://github.com/ValentinFurcy/OnEVa_test.git'
      }
    }

    stage('Restore NuGet Packages') {
        agent {
            docker {
                image 'mcr.microsoft.com/dotnet/sdk:8.0'  // Utilise l'image Docker officielle du SDK .NET
                args '-v /root/.nuget/packages:/root/.nuget/packages'  // Monte le cache NuGet pour la persistance
            }
        }
        steps {
            sh 'dotnet restore'
        }

    stage('Build') {
      steps {
        sh 'dotnet build --configuration Release'
      }
    }

    stage('Test') {
      steps {
         sh'dotnet test Test_CI.csproj'
      }
    }

    stage('Publish') {
      steps {
        sh 'dotnet publish --configuration Release --output ./publish'
      }
    }
  }

  post {
    success {
      junit 'TestResults/*.trx'
      echo 'Notifying GitHub of successful build...'
    }
    failure {
      echo 'Notifying GitHub of failed build...'
    }
  }
}
