# IA_CONTEXT.md
# Sistema de Gestión Electoral (SGE)

> Este documento contiene el contexto completo del proyecto para que cualquier conversación nueva con ChatGPT pueda continuar el desarrollo sin necesidad de volver a explicar el sistema.

---

# PROYECTO

Nombre:

Sistema de Gestión Electoral (SGE)

Descripción:

El Sistema de Gestión Electoral (SGE) es una aplicación web destinada a administrar referentes, votantes y movilizadores utilizando como base un padrón electoral.

No es un sistema para administrar ciudadanos.

Todas las personas YA EXISTEN previamente dentro de un padrón electoral.

El sistema solamente organiza esas personas.

---

# OBJETIVO

El objetivo es permitir:

- Registrar referentes.
- Asignar personas del padrón a un referente.
- Definir si esa persona será:
    - Votante
    - Movilizador
- Obtener reportes por:
    - Referente
    - Escuela
    - Circuito
    - Mesa

---

# REGLAS DE NEGOCIO

Estas reglas NO deben romperse.

1.

Toda persona proviene del padrón.

2.

Nunca se crea una persona manualmente.

3.

La búsqueda principal es por DNI.

4.

Un referente también es una persona del padrón.

5.

Un movilizador también es una persona del padrón.

6.

Un votante también es una persona del padrón.

7.

Una persona solamente puede pertenecer a UN referente.

8.

No puede existir una persona asignada a dos referentes.

9.

Si la persona es movilizador:

Debe tener:

Vehículo

Patente

10.

Los datos del padrón NO pueden editarse desde el sistema.

---

# TECNOLOGÍAS

Backend

- ASP.NET Core MVC (.NET 8)

Lenguaje

- C#

Base de datos

- PostgreSQL

ORM

- Entity Framework Core

Frontend

- Bootstrap 5
- HTML
- CSS
- JavaScript

Bibliotecas

- ClosedXML
- Chart.js

Control de versiones

- Git
- GitHub

---

# ARQUITECTURA

La arquitectura es MVC.

Debe respetarse la separación de responsabilidades.

Estructura:

Controllers

Models

Views

Data

Repositories

Services

DTOs

Helpers

wwwroot

Nunca mezclar responsabilidades.

Nunca acceder directamente a la base desde las Views.

La lógica debe pasar por:

Controller

↓

Service

↓

Repository

↓

DbContext

---

# ESTRUCTURA GENERAL DEL SISTEMA

Login

↓

Dashboard

↓

Referentes

↓

Detalle Referente

↓

Asignaciones

↓

Reportes

↓

Usuarios

↓

Configuración

---

# FLUJO PRINCIPAL

1.

Importar padrón.

↓

2.

Buscar persona por DNI.

↓

3.

Agregar como referente.

↓

4.

Buscar otra persona por DNI.

↓

5.

Asignarla al referente.

↓

6.

Elegir:

Votante

o

Movilizador.

↓

7.

Si es movilizador:

Registrar vehículo y patente.

↓

8.

Guardar.

---

# MÓDULOS

Dashboard

Referentes

Asignaciones

Reportes

Usuarios

Configuración

Importador del padrón

---

# MODELO CONCEPTUAL

Persona

↓

Referente

↓

Asignación

↓

Rol

↓

Votante

o

Movilizador

---

# BASE DE DATOS

Las entidades principales serán:

Persona

Referente

Asignacion

Usuario

Importacion (más adelante)

Auditoria (más adelante)

---

# PERSONA

Representa el padrón.

Campos principales:

DNI

ApellidoNombre

Sexo

Domicilio

Circuito

Localidad

Departamento

Escuela

Mesa

Orden

Cambio

Observaciones

---

# REFERENTE

Representa una persona convertida en referente.

Contiene solamente información adicional.

Nunca duplica datos del padrón.

---

# ASIGNACION

Relaciona:

Referente

↓

Persona

↓

Rol

Rol:

Votante

Movilizador

---

# DASHBOARD

Debe mostrar:

Cantidad de referentes.

Cantidad de votantes.

Cantidad de movilizadores.

Cantidad de escuelas.

Cantidad de circuitos.

Gráficos.

---

# REPORTES

Debe permitir filtrar por:

Referente

Escuela

Circuito

Mesa

Rol

Debe exportar:

Excel

PDF

---

# IMPORTADOR DEL PADRÓN

El sistema NO dependerá de importar datos manualmente desde PostgreSQL.

Existirá un módulo:

Configuración

↓

Importar padrón

↓

Seleccionar Excel

↓

Validar columnas

↓

Importar

↓

Resumen

El padrón podrá actualizarse sin modificar el código.

---

# GIT

Repositorio:

GitHub

Rama principal:

main

Nunca trabajar directamente sobre main.

Utilizar ramas feature.

Ejemplo:

feature/dashboard

feature/importador-padron

feature/referentes

feature/reportes

---

# ORGANIZACIÓN DEL EQUIPO

El proyecto es desarrollado por dos personas.

Persona 1

Backend

Responsabilidades:

PostgreSQL

Entity Framework

Migraciones

Services

Repositories

Importación Excel

Reglas de negocio

DTOs

DbContext

No modificar Frontend.

------------------------------------

Persona 2

Frontend

Responsabilidades:

Bootstrap

Views MVC

Layout

Dashboard

CSS

JavaScript

UX/UI

Reportes visuales

No modificar Backend.

---

# CHAT PRINCIPAL

Existe un chat principal destinado únicamente a:

Arquitectura.

Diseño.

Modelo de datos.

Decisiones importantes.

Organización.

No utilizarlo para desarrollar funcionalidades específicas.

---

# CONVENCIONES

Siempre explicar las decisiones.

No modificar arquitectura sin justificarlo.

Trabajar paso a paso.

Primero explicar.

Luego programar.

Después probar.

Luego continuar.

No generar código innecesario.

Priorizar claridad sobre complejidad.

Pensar siempre como un Arquitecto de Software Senior.

---

# ESTADO ACTUAL DEL PROYECTO

✔ Proyecto ASP.NET Core MVC creado.

✔ Git configurado.

✔ GitHub configurado.

✔ PostgreSQL instalado.

✔ Entity Framework instalado.

✔ Estructura MVC creada.

✔ Carpetas:

Controllers

Models

Views

Data

Repositories

Services

DTOs

Helpers

wwwroot

✔ Desarrollo inicial del modelo de datos.

⏳ Pendiente:

Conectar completamente PostgreSQL.

Crear migraciones.

Importador Excel.

Dashboard.

Gestión de Referentes.

Asignaciones.

Reportes.

Usuarios.

---

# IMPORTANTE

Si este contexto ya fue leído, NO volver a proponer una arquitectura completamente distinta.

Siempre continuar respetando las decisiones tomadas anteriormente.

Actuar como si formaras parte del equipo de desarrollo desde el inicio del proyecto.
---

# HISTORIAL DEL CONTEXTO

## v1.0

- Proyecto creado.
- Arquitectura definida.
- Tecnologías definidas.
- Reglas de negocio definidas.
- Organización Backend / Frontend definida.
