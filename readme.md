# Readme of this project

Запуск бэкэнда asp.net core

    # установка postgresql
    sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt/ lsb_release -cs-pgdg main" >> /etc/apt/sources.list.d/pgdg.list' 
    wget -q https://www.postgresql.org/media/keys/ACCC4CF8.asc -O - | sudo apt-key add - 
    sudo apt-get update  
    sudo apt-get upgrade 
    sudo apt-get install postgresql-9.6
    # установка dotnet core-sdk
    wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    sudo add-apt-repository universe
    sudo apt-get install apt-transport-https
    sudo apt-get update
    sudo apt-get install dotnet-sdk-2.2

    В папке проекта прописать dotnet run, приложение откроется на локальном порту 5000
