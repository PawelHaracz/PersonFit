# PersonFit

Ustrukturyzowany monolit składający się z modułów
- Exercise 
- Planner

## Migracja 
Migracje tworzy się za pomocą komendy:

```bash
dotnet ef migrations add PlannerInit --context PostgresPlannerDomainContext -p PersonFit.Domain.Planner --startup-project PersonFit
```

## Lokalny środowisko
```bash
cd PersonFit/infra
docker-compose up 
```

### budowanie obrazu
```shell
 docker build -t personfit:0.2  .
```