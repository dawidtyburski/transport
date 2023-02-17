## Giełda Transportowa

---

**Opis:** Poniższa dokumentacja opisuje działanie systemu giełdy transportowej. Pozwala on na generowanie zleceń transportowych i udostępnianie ich do wglądu wizytującym stronę. 

**Przykładowi użytkownicy:**

**Super Admin:**  
Email: superadmin@cargo.eu  
Hasło: Secret123@

**Admin:**

Email: admin@cargo.eu  
Hasło: Secret123@

**User:**

Email: ppotoczak@cargo.eu  
Hasło: Secret123@

**User(blocked):**

Email: akowalski@cargo.eu  
Hasło Secret123@

**connectionString** = "Server=(localdb)\\\\mssqllocaldb;Database=TransportDb;Trusted\_Connection=True;"

**Strona główna dla użytkowników niezalogowanych**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/b8b17e3adbbdc6860c74af69d3479230d6a0ebd49ff06248.png)

**Użytkownik niezalogowany ma dostęp do wyszukiwarki zleceń transportowych.**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/001b64d6a2f86234bddb65a423667a622a69e4804203682c.png)

**Filtrowanie wyników:**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/78137e580d6c0049fceefa3f0ec1bd2a908fa54cb2a68795.png)

**Może również wyświetlić wszystkie zlecenia naciskając przycisk “Search All”. Po kliknięciu w konkretne zlecenie rozwija się wiersz z dodatkowymi informacjami.**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/a008ec3d1b12f49859ae48065e63f4b0138201b17e304bac.png)

**Rejestracja:**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/e3073220c864e7aa062d6ed827ee9f92efc9ea390813eff7.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/f45d49821543e7b785d719c76697f352f41e27f536eab100.png)

**Logowanie:**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/5e8636f7c6ae425c2557bd3b0cacb0dafab6983e2d3a85a1.png)

**Próba zalogowania przez zablokowanego użytkownika:**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/b486ed2ed3f20f79e2e245b527f7568e88d0f1a4fa344968.png)

**Strona główna po zalogowaniu:**

Funkcja “Create” zastępuje “Log in”

Użytkownik zalogowany ma dostęp do tworzenia zleceń, wyświetlania listy swoich zleceń, wyświetlania rankingu użytkowników oraz zamiany hasła (po kliknięciu w imię i nazwisko w nav-barze)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/b6b54eaad90b019fc58c1d2aad6f48861c33427aa49c4f4e.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/dedf121658e23fa3e885f791403ebe6346aea94083fd9ea4.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/70b0e6b9cc6ea8e337843b5144624db26d21fa1c2ef71ad9.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/179df0fa0c4386774b30c5965f3129f36d7cd717ee091fc0.png)

W widoku swoich zleceń użytkownik po kliknięciu w konkretne zlecenie może skorzystać z dodatkowych opcji “Edit” oraz “Delete”

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/c3a5e1274563a281918e0e5e86b93c0ea3c60cd55ff33114.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/836e067a58d2874f1bbbe52394e516c1c258a8cff09c7610.png)

**Użytkownik z rolą “Admin” ma dodatkowo dostęp do Panelu Administratora ze szczegółowymi danymi użytkowników oraz funkcji blokowania**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/003a0f08cbca2acfaff671fcee81045cc0dc7a36c66cd09c.png)

**Użytkownik z rolą “SuperAdmin” ma dostęp do tego samego panelu z dodatkową funkcją zmiany roli użytkowników:**

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/7dbb2162cb2c9c978f447d6653ad0aaebef8623c0b3525db.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/655e4f5ee1635feb0829c023fa0c3425ab823447f70804b1.png)
