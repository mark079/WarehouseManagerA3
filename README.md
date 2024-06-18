# Warehouse Manager

Este é o projeto Warehouse Manager, uma aplicação para gerenciar armazéns.

## Requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Instruções para subir o projeto

1. **Clone o repositório:**

    ```bash
    git clone git@github.com:mark079/WarehouseManagerA3.git
    cd WarehouseManagerA3
    ```

2. **Instale as dependências:**

    Se você não possui o Entity Framework Core instalado, rode o comando abaixo para instalá-lo:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

3. **Configure a Connection String:**

    Abra o arquivo `appsettings.json` e edite a `ConnectionStrings` para apontar para o seu banco de dados. A estrutura deve ser semelhante a esta:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=warehouse_manager;User=admin;Password=admin;"
    }
    ```

4. **Atualize o banco de dados:**

    Rode o comando abaixo para aplicar as migrações e configurar o banco de dados:

    ```bash
    dotnet ef database update
    ```

5. **Execute a aplicação:**

    Finalmente, rode o comando abaixo para iniciar a aplicação:

    ```bash
    dotnet run
    ```


## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
