# Sistema de Gestión Electoral (SGE)

## Descripción

Sistema web para la administración de referentes, votantes y movilizadores utilizando un padrón electoral existente.

El sistema permitirá organizar territorialmente la información electoral mediante relaciones entre referentes y personas del padrón.

---

# Tecnologías

Backend

- ASP.NET Core MVC (.NET 8)

Frontend

- Bootstrap 5
- HTML
- CSS
- JavaScript

Base de datos

- PostgreSQL

ORM

- Entity Framework Core

Bibliotecas

- ClosedXML
- Chart.js
- QuestPDF (pendiente)

Control de versiones

- Git
- GitHub

---

# Arquitectura

MVC

Controllers

Models

Views

Services

Repositories

DTOs

Helpers

Data

---

# Estado actual

Proyecto ASP.NET MVC creado.

Git configurado.

Repositorio GitHub creado.

PostgreSQL instalado.

Entity Framework instalado.

Se comenzará el diseño del modelo de datos.

---

# Objetivo

El objetivo principal es administrar:

Referentes

↓

Votantes

↓

Movilizadores

mediante un padrón electoral previamente cargado.

---

# Regla principal

Todas las personas provienen del padrón.

No existen altas manuales.

Todas las búsquedas se realizan mediante DNI.

Una persona solamente puede pertenecer a un referente.

---

# Módulos

Dashboard

Referentes

Asignaciones

Reportes

Usuarios

Configuración

---

# Integrantes

Persona 1

Backend

Persona 2

Frontend

---

# Arquitecto del proyecto

Chat principal de ChatGPT.

Aquí se toman las decisiones de arquitectura.

Los chats Backend y Frontend solamente implementan.
