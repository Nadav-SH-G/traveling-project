document.addEventListener('DOMContentLoaded', function () {
    function RformSubmit() {
        var res = true;
        res = checkUsername() && res;
        res = checkPassword() && res;
        res = SCheckPassword() && res;
        res = fNameCheck() && res;
        res = LNameCheck() && res;
        res = brithdayCheck() && res;
        res = phoneCheck() && res;
        res = emailCheck() && res;
        console.log("Form validation result: ", res); 
        return res;
    }

    function checkUsername() {
        const regex = /^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+$/;
        var username = document.getElementById("userName").value;
        if (username.length < 2) {
            document.getElementById("eUserName").innerHTML = "שם משתמש צריך להכיל שני תווים לפחות*";
            return false;
        }
        if (!regex.test(username)) {
            document.getElementById("eUserName").innerHTML = "מותר רק אותיות באנגלית, מספרים ותווים מיוחדים*";
            return false;
        }
        document.getElementById("eUserName").innerHTML = "";
        return true;
    }

    function checkPassword() {
        const regex = /^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+$/;
        const regex2 = /^(?=(.*[A-Z]))(?=(.*\d))(?=(.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]))(?!.*([a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?])\1\1).*$/;

        var password = document.getElementById("Password").value;
        if (password.length < 6 || password.length > 12) {
            document.getElementById("ePassword").innerHTML = "הסיסמא חייבת להכיל בין 6-12 תווים*";
            return false;
        }
        if (!regex.test(password)) {
            document.getElementById("ePassword").innerHTML = "מותר רק אותיות באנגלית, מספרים ותווים מיוחדים*";
            return false;
        }
        if (!regex2.test(password)) {
            document.getElementById("ePassword").innerHTML = "חייב להכיל לפחות אות אחת גדולה, תו מיוחד אחד, מספר אחד, וללא שלושה תווים זהים*";
            return false;
        }
        document.getElementById("ePassword").innerHTML = "";
        return true;
    }

    function SCheckPassword() {
        var password = document.getElementById("Password").value;
        var Spassword = document.getElementById("SPassword").value;

        if (Spassword.length === 0) {
            document.getElementById("eSPassword").innerHTML = "שדה חובה*";
            return false;
        }
        if (password !== Spassword) {
            document.getElementById("eSPassword").innerHTML = "הסיסמאות לא זהות*";
            return false;
        }
        document.getElementById("eSPassword").innerHTML = "";
        return true;
    }

    function fNameCheck() {
        var regex3 = /^[A-Za-z\u0590-\u05FF\b]{2,}$/;
        var Fname = document.getElementById("fName").value;
        if (!regex3.test(Fname)) {
            document.getElementById("eFName").innerHTML = "חובה 2 אותיות מינימום באנגלית או עברית *";
            return false;
        }
        document.getElementById("eFName").innerHTML = "";
        return true;
    }

    function LNameCheck() {
        var regex3 = /^[A-Za-z\u0590-\u05FF\b]{2,}$/;
        var Lname = document.getElementById("Lname").value;
        if (!regex3.test(Lname)) {
            document.getElementById("eLname").innerHTML = "חובה 2 אותיות מינימום באנגלית או עברית *";
            return false;
        }
        document.getElementById("eLname").innerHTML = "";
        return true;
    }

    function brithdayCheck() {
        var date = document.getElementById("birthdayDate").value;
        if (date === "") {
            document.getElementById("eBirthDay").innerHTML = "שדה חובה *";
            return false;
        }
        document.getElementById("eBirthDay").innerHTML = "";
        return true;
    }

    function phoneCheck() {
        var phone = document.getElementById("phone").value;
        const regex = /^(0[2-49]{1}[0-9]{1}|05|07)[0-9]{1}-[0-9]{7}$/; 
        if (!regex.test(phone)) {
            document.getElementById("ePhone").innerHTML = "טלפון חייב להיות חוקי (כולל מקף ומספרים נכונים)*";
            return false;
        }
        document.getElementById("ePhone").innerHTML = "";
        return true;
    }


    function emailCheck() {
        var email = document.getElementById("email").value;
        const regex = /^[a-zA-Z][a-zA-Z0-9_-]*@[a-zA-Z][a-zA-Z0-9_-]*\.[a-zA-Z]{2,3}$/;
        if (email.length === 0) {
            document.getElementById("eEmail").innerHTML = "דוא"ל הוא שדה חובה * ";
            return false;
        }
        if (!regex.test(email)) {
            document.getElementById("eEmail").innerHTML = "הדוא"ל לא חוקי, בדוק את הפורמט * ";
            return false;
        }
        document.getElementById("eEmail").innerHTML = "";
        return true;
    }
   
    document.querySelector("form").onsubmit = function (event) {
        console.log("Form submitted");
        if (!formSubmit()) {
            event.preventDefault(); 
            console.log("Form submission blocked");
        }
    };
});
