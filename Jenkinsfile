pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git branch: 'Stage', url: 'https://github.com/PranayKalgutkar/BookHiveAPI.git'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploying...'
                // Add deployment commands here
            }
        }
    }
}