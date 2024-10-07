pipeline {
  agent any

  tools {
    dotnetsdk 'sdk.NET8.0.8'  // Nom de ton installation .NET SDK configurée dans Jenkins
  }

  stages {

    // Étape 1 : Nettoyer l'espace de travail
    stage('Clean Workspace') {
      steps {
        cleanWs()  // Nettoie l'espace de travail
      }
    }

    // Étape 2 : Cloner le dépôt GitHub
    stage('Checkout') {
      steps {
        git branch: 'main', url: 'https://github.com/ValentinFurcy/OnEVa_test.git'
      }
    }

    // Étape 3 : Restaurer les dépendances
    stage('Restore NuGet Packages') {
      steps {
        sh 'dotnet restore'
      }
    }

    // Étape 4 : Construire le projet
    stage('Build') {
      steps {
        sh 'dotnet build --configuration Release'
      }
    }

    // Étape 5 : Exécuter les tests
    stage('Test') {
      steps {
         sh'dotnet test Test_CI.csproj'
      }
    }

    // Étape 6 : Publier le projet
    stage('Publish') {
      steps {
        sh 'dotnet publish --configuration Release --output ./publish'
      }
    }
  }

  // Notification en cas de succès ou d'échec
  post {
    success {
      echo 'Le build a été réalisé avec succès.'
    }
    failure {
      echo 'Le build a échoué.'
    }
  }
}
