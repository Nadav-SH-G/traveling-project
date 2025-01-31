function formSubmit() {
    var res = true;
    res = checkUsername() && res;
    res = checkPassword() && res;
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

        document.getElementById("eUserName").innerHTML = "מותר רק אותיות באנגלית,מספרים ותווים מיוחדים*";
        return false;
    }
    else {
        document.getElementById("eUserName").innerHTML = "";
    }

    return true;
}

function checkPassword() {
    const regex = /^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+$/;
    const regex2 = /^(?=(.*[A-Z]))(?=(.*\d))(?=(.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]))(?!.*([a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?])\1\1).*$/;

    var password = document.getElementById("Password").value;
    if (password.length <6 || password.length >12) {
        document.getElementById("ePassword").innerHTML = "הסיסמא חייבת להכיל בין 6-12 תווים*";
        return false;9
    }
    else if (!regex.test(password)) {

        document.getElementById("ePassword").innerHTML = "מותר רק אותיות באנגלית,מספרים ותווים מיוחדים*";
        return false;
    }
    else if (!regex2.test(password)) {

        document.getElementById("ePassword").innerHTML = "חייב להכיל לפחות אות אחת גדולה, תו מיוחד אחד, מספר אחד, וללא שלושה תווים זהים*";
        return false;
    }
    else {
        document.getElementById("ePassword").innerHTML = "";
    }

    return true;
}
