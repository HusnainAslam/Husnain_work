$(document).ready(function () {
               $('#cnic').keydown(function () {

                   //allow  backspace, tab, ctrl+A, escape, carriage return
                   if (event.keyCode == 8 || event.keyCode == 9
                                     || event.keyCode == 27 || event.keyCode == 13
                                     || (event.keyCode == 65 && event.ctrlKey === true))
                       return;
                   if ((event.keyCode < 48 || event.keyCode > 57))
                       event.preventDefault();

                   var length = $(this).val().length;

                   if (length == 5 || length == 13)
                       $(this).val($(this).val() + '-');

               });

               
               
});
function checkPass() {
    //Store the password field objects into variables ...
    var pass1 = document.getElementById('pass1');
    var pass2 = document.getElementById('pass2');
    //Store the Confimation Message Object ...
    var message = document.getElementById('confirmMessage');
    //Set the colors we will be using ...
    var goodColor = "#66cc66";
    var badColor = "#ff6666";
    //Compare the values in the password field 
    //and the confirmation field
    if (pass1.value == pass2.value) {
        //The passwords match. 
        //Set the color to the good color and inform
        //the user that they have entered the correct password 
        pass2.style.backgroundColor = goodColor;
        message.style.color = goodColor;
        message.innerHTML = "Passwords Match!"
    } else {
        //The passwords do not match.
        //Set the color to the bad color and
        //notify the user.
        pass2.style.backgroundColor = badColor;
        message.style.color = badColor;
        message.innerHTML = "Passwords Do Not Match!"
    }
}