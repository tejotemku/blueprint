# Blueprint

## Web Aplication only mode (no server services)

### Prerequisites
- Node.js >= 16.13

### Setup
Webapp files are stored in `frontend` folder.
```bash
cd frontend/
```

### Instalation
Node packages need to be installed.
```bash
npm install
```

### Running
After instalation is completed, run the web aplication with command below:
```bash 
npm run serve
```

Web aplication will be hosted on port 8080 by default.


## Docker mode
### Prerequisites
- ASP.NET >= 6.0
- Node.js >= 16.13
- docker >= 20.10

### Instalation
```bash
docker-compose up --build
```

