# GeoPet

<details>
  <summary>Table of Contents</summary>
 <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#technologies">Technologies</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#setup">Setup</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
     <ul>
        <li><a href="#tests">Tests</a></li>
      </ul>
  </ol>
</details>

## Sobre o projeto

Projeto final da Aceleração em C# que cria uma API que permita cadastrar pets e seus donos,
alem de permitir uma rastreio do pet via coodenadas de GPS.

### Tecnologias

![C#][c#]
![MicrosoftSQLServer][microsoftSQLServer]
![.Net][.net]

</details>

## Getting Started

### Setup

- Clone the repository and switch to it

  ```bash
  git clone https://github.com/imarmendes/GeoPet/ && cd GeoPet/src
  ```

- Install packages

  ```bash
  dotnet restore
  ```

- Configure MSQServer with docker

  ```bash
  docker-compose up -d && dotnet ef migrations add Setup_Migration --project GeoPet && dotnet ef database update --project GeoPet
  
  ```

- Run the app

  ```bash
  dotnet run --project GeoPet/GeoPet.csproj
  ```

## Usage

### Tests

  ```bash
  dotnet test
  ```

---

[⬆ Back to top](#geopet)  

<!-- MARKDOWN LINKS & IMAGES -->

[c#]: https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white
[microsoftSQLServer]: https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white
[.net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
