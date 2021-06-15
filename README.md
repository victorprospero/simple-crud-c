### Backend Test

A simple CRUD RESTFul WebApi

* .NET Core
* Swagger UI
* MVC Layered Architecture
* FluentValidations
* Persistence in static variable
* xUnit Unit Tests

### Product payload:

```json
{
    "sku": 43264,
    "name": "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g",
    "inventory": {
        "quantity": 15,
        "warehouses": [
            {
                "locality": "SP",
                "quantity": 12,
                "type": "ECOMMERCE"
            },
            {
                "locality": "MOEMA",
                "quantity": 3,
                "type": "PHYSICAL_STORE"
            }
        ]
    },
    "isMarketable": true
}
```

### Requirements

- **inventory.quantity** returns a sum of the warehouses quantity;

- **product.isMarketable** returns TRUE if its inventory.quantity is greater than 0;
