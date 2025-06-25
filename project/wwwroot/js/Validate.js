function validate() {
    var res = true;
    res = firstNameVal() && res;
    res = lastNameVal() && res;
    res = usernameVal() && res;
    res = passwordVal() && res;
    res = emailVal() && res;
    res = phoneVal() && res;
    res = birthdayVal() && res;
    // You might not need to validate Admin (checkbox)

    if (res) {
        return true;
    }
    return false;
}


function firstNameVal() {
    var fName = document.getElementById("FirstName");
    var msg = document.getElementById("FNameMsg");

    if (fName == null) {
        console.error("Could not find the First Name input element!");
        return false;
    }

    if (fName.value.trim() === "") {
        msg.innerHTML = "You must enter the first name!";
        return false;
    }
    msg.innerHTML = "";
    return true;
}

function lastNameVal() {
    var lName = document.getElementById("LastName");
    var msg = document.getElementById("LNameMsg");

    if (lName == null) {
        console.error("Could not find the Last Name input element!");
        return false;
    }

    if (lName.value.trim() === "") {
        msg.innerHTML = "You must enter the last name!";
        return false;
    }
    msg.innerHTML = "";
    return true;
}

function usernameVal() {
    var uName = document.getElementById("Username");
    var msg = document.getElementById("UNameMsg");

    if (uName == null) {
        console.error("Could not find the Username input element!");
        return false;
    }

    if (uName.value.trim() === "") {
        msg.innerHTML = "You must enter a username!";
        return false;
    }
    msg.innerHTML = "";
    return true;
}

function passwordVal() {
    var pWord = document.getElementById("Password");
    var msg = document.getElementById("PasswordMsg");

    if (pWord == null) {
        console.error("Could not find the Password input element!");
        return false;
    }

    if (pWord.value.trim() === "") {
        msg.innerHTML = "You must enter a password!";
        return false;
    }
    msg.innerHTML = "";
    return true;
}

function emailVal() {
    var email = document.getElementById("Email");
    var msg = document.getElementById("EmailMsg");
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (email == null) {
        console.error("Could not find the Email input element!");
        return false;
    }

    if (email.value.trim() === "") {
        msg.innerHTML = "You must enter an email address!";
        return false;
    }

    if (!emailRegex.test(email.value)) {
        msg.innerHTML = "Please enter a valid email address!";
        return false;
    }

    msg.innerHTML = "";
    return true;
}

function phoneVal() {
    var phone = document.getElementById("Phone");
    var msg = document.getElementById("PhoneMsg");
    const regex = /^(0[2-49]{1}[0-9]{1}|05|07)[0-9]{1}-[0-9]{7}$/; 

    if (phone == null) {
        console.error("Could not find the Phone input element!");
        return false;
    }

    if (phone.value.trim() === "") {
        msg.innerHTML = "You must enter a phone number!";
        return false;
    }

    if (!regex.test(phone.value)) {
        msg.innerHTML = "Please enter a valid 10-digit phone number!";
        return false;
    }

    msg.innerHTML = "";
    return true;
}

function birthdayVal() {
    var birthday = document.getElementById("Birthday");
    var msg = document.getElementById("BirthdayMsg");

    if (birthday == null) {
        console.error("Could not find the Birthday input element!");
        return false;
    }

    if (birthday.value.trim() === "") {
        msg.innerHTML = "Please select a birthday!";
        return false;
    }
    msg.innerHTML = "";
    return true;
}