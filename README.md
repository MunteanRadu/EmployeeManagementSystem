# Employee Management System API

- [Employee Management System API](#employee-management-system-api)
  - [Create Employee](#create-employee)
    - [Create Employee Request](#create-employee-request)
    - [Create Employee Response](#create-employee-response)
  - [Get Employee](#get-employee)
    - [Get Employee Request](#get-employee-request)
    - [Get Employee Response](#get-employee-response)
  - [Update Employee](#update-employee)
    - [Update Employee Request](#update-employee-request)
    - [Update Employee Response](#update-employee-response)
  - [Delete Employee](#delete-employee)
    - [Delete Employee Request](#delete-employee-request)
    - [Delete Employee Response](#delete-employee-response)

## Create Employee

### Create Employee Request

```js
POST /Employees
```

```json
{
    "firstName": "Mark",
    "lastName": "Fishbach",
    "age": 27,
    "startDateTime": "2023-08-08T08:00:00",
    "dateOfBirth": "1996-01-01T11:00:00",
    "skillset": [
        "Communication",
        "Leadership",
        "Fast Learner",
        "Python"
    ]
}
```

### Create Employee Response

```js
201 Created
```

```yml
Location: {{host}}/Employees/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "Mark",
    "lastName": "Fishbach",
    "age": 27,
    "startDateTime": "2023-08-08T08:00:00",
    "dateOfBirth": "1996-01-01T11:00:00",
    "lastModifiedDateTime": "2023-08-08T12:00:00",
    "skillset": [
        "Communication",
        "Leadership",
        "Fast Learner",
        "Python"
    ]
}
```

## Get Employee

### Get Employee Request

```js
GET /Employees/{{id}}
```

### Get Employee Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "Mark",
    "lastName": "Fishbach",
    "age": 27,
    "startDateTime": "2023-08-08T08:00:00",
    "dateOfBirth": "1996-01-01T11:00:00",
    "lastModifiedDateTime": "2023-08-08T12:00:00",
    "skillset": [
        "Communication",
        "Leadership",
        "Fast Learner",
        "Python"
    ]
}
```

## Update Employee

### Update Employee Request

```js
PUT /Employees/{{id}}
```

```json
{
    "firstName": "Mark",
    "lastName": "Fishbach",
    "age": 27,
    "startDateTime": "2023-08-08T08:00:00",
    "dateOfBirth": "1996-01-01T11:00:00",
    "skillset": [
        "Communication",
        "Leadership",
        "Fast Learner",
        "Python"
    ]
}
```

### Update Employee Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/Employees/{{id}}
```

## Delete Employee

### Delete Employee Request

```js
DELETE /Employees/{{id}}
```

### Delete Employee Response

```js
204 No Content
```
