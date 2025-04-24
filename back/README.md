# 🛠️ Product-trial API

## 🚀 Getting Started

Before running the API, you need to apply the database migrations and set the required environment variables.

---

### 📦 1. Apply EF Core Migration

Make sure you have the Entity Framework CLI installed:

```bash
dotnet tool install --global dotnet-ef
```

Then in the project folder, run the migrations using the following command :
```bash
dotnet ef database update -p .\Product-trial.DAL -s .\Product-trial
```

### 📦 2. Configure the app

Create a JWT Security key.

Set the <b>Auth__JwtSecret</b> variable in the Properties/launchSettings.json file with a 128 bit base64 encoded key.

## Should be good to go