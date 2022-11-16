

# Ayuda. Commandos utilizados con visual code. 

## Dependencias para utilizar migraciones. 
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

## Commando para agregar una nueva migracion
dotnet ef migrations add InitialCreate

## Commado para actualizar nuestra base de datos, con las migraciones
dotnet ef database update


# Si se trabaja con visual studio


## Agregar migracion
add-migration Inicial -Context ComercioElectronicoDbContext

## Aplicar migracion
Update-Database -Context ComercioElectronicoDbContext 

## Realizar migracion por script
Script-Migration -Context ComercioElectronicoDbContext -From Inicial

## Genera script desde la primera migracion hasta la ultima
Script-Migration -Context ComercioElectronicoDbContext 0


