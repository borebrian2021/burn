//const dev_domain = "https://localhost:44349/"
const dev_domain = "https://www.panafricanburns.com/"

$(document).ready(function () {
    $("#loading").hide();
    $("#signup").fadeOut();
    //alert("Test");
  
});
function signup() {
    $("#signup").show();
    $("#login").fadeOut();
}
function login() {
    $("#login").show();
    $("#signup").fadeOut();
}

// Event handler for form submission
$('#contactform').submit(function (event) {
    const endPoint = dev_domain +"api/upoad";
    event.preventDefault();
  
    // Prevent the form from submitting the traditional way
    const email = $('#email_l').val();
    const Password_l = $('#Password_').val();
    const rememberMe = $('#rememberMe').prop('checked');
    // Prepare the data to be sent in the request
    const data = {
        fullnames: "The Real OG",
        email: $("#email_l").val(),
        password: $("#Password_").val(),
        role: "1",
        phone: "+254RutoMustGo",
        isconfirmed: false,
        rememberMe: rememberMe,
        country: "No Mans Land"
    };

    // Use fetch to send the POST request
    fetch(endPoint, {  // Replace with your actual API URL
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            if (data.status) {
                // Store the token in localStorage or sessionStorage
                //localStorage.setItem('JWToken', data.token);

                // Optionally, redirect the user to a new page (e.g., dashboard)

                Swal.fire({
                    icon: "success",
                    title: "Login success!",
                    text: data.status
                });
                window.location.href = '/home/dashboard';
                // Replace with your desired redirect URL
            } else {
                // Handle login failure
                Swal.fire({
                    icon: "warning",
                    title: "Login fail",
                    text: data.message,
                    footer: '<a href="#">Reset password</a>'
                });
            }
        })

});
// Event handler for form submission
$('#register').submit(function (event) {
    const endPoint = dev_domain +"api/register/";
    event.preventDefault();
    // Prevent the form from submitting the traditional way
    const email = $('#email_l').val();
    const Password_l = $('#Password_').val();
    const rememberMe = $('#rememberMe').prop('checked');
    // Prepare the data to be sent in the request
    const data = {
        fullnames: $("#name").val(),
        email: $("#email").val(),
        password: $("#rpassword").val(),
        role: "1",
        phone: $("#phonenumber").val(),
        isconfirmed: false,
        rememberMe: rememberMe,
        country: $("#country").val(),
    };

    // Use fetch to send the POST request
    fetch(endPoint, {  // Replace with your actual API URL
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            if (data.success) {
                // Store the token in localStorage or sessionStorage
                //localStorage.setItem('JWToken', data.token);

                // Optionally, redirect the user to a new page (e.g., dashboard)

                Swal.fire({
                    icon: data.status,
                    title: "Registration successful!",
                    text: data.message
                });
                $("#signup").fadeOut();
                $("#admin").fadeOut();
             
                // Replace with your desired redirect URL
            } else {
                // Handle login failure
                Swal.fire({
                    icon: data.status,
                    title: "Registration fail",
                    text: data.message,
                    footer: '<a href="#">Reset password</a>'
                });
            }
        })

});










//Signup
$("#contactus").submit(function (event) {
    event.preventDefault(); // Prevent form submission to avoid page refresh

    const apiEndpoint = dev_domain+"api/contactus";

    // Define the data to be sent to the API
    const registerData = {
        fullnames: $("#name").val(),
        email: $("#email").val(),
        Number: $("#number").val(),
        subject: $("#subject").val(),
        Date: $("#date").val(),
        message: $("#message").val(),
        
    };

    // Send the POST request
    fetch(apiEndpoint, {
        method: "POST", // HTTP method
        headers: {
            "Content-Type": "application/json" // Inform server that the payload is JSON
        },
        body: JSON.stringify(registerData) // Convert JS object to JSON string
    })
        .then(response => {
            if (!response.status == "success") {
                Swal.fire({
                    icon: "error",
                    title: "Registration failed.",
                    text: response,
                    footer: '<a href="#">Why do I have this issue?</a>'
                });
            }
            else {
                Swal.fire({
                    title: "Success!",
                    text: "Registration successful!!",
                    icon: "success"
                });
                $("#login").show();
                $("#signup").fadeOut();
            }
            return response.json(); // Parse the JSON response
        })
        .then(data => {
            console.log("Response from server:", data); // Log the response data
        })
        .catch(error => {
            console.log("Error posting data:", error); // Log any errors
        });

});
//Signup
$("#upload_abstract").submit(function (event) {
    event.preventDefault(); // Prevent form submission to avoid page refresh
    $("#uploadDoc").fadeOut();
    $("#loading").fadeIn();

    const apiEndpoint = dev_domain + "api/upload";
    // Define the data to be sent to the API
    const registerData = {
        FirstName: $("#fname").val(),
        LastName: $("#lname").val(),
        institution: $("#institution").val(),
        Email: $("#email_l").val(),
        Phone: $("#Pnumber").val(),
        Abstract: $("#abstract").val(),

    };

    // Send the POST request
    fetch(apiEndpoint, {
        method: "POST", // HTTP method
        headers: {
          
            'Content-Type': 'application/json'
            // Inform server that the payload is JSON
        },
        body: JSON.stringify(registerData) // Convert JS object to JSON string
    })
        .then(response => {
            if (!response.status == "success") {
                Swal.fire({
                    icon: "error",
                    title: "Upload failed failed.",
                    text: response,
                    footer: '<a href="#">Why do I have this issue?</a>'
                });

 

            }
            else {
                Swal.fire({
                    title: "Success!",
                    text: "Abstract Uploaded  successfully!!",
                    icon: "success"
                });
                $("#uploadDoc").fadeIn();
                $("#loading").fadeOut();
            
                $("input:text").val("");
                $("textarea:text").val("");
            }
            return response.json(); // Parse the JSON response
        })
        .then(data => {
            console.log("Response from server:", data); // Log the response data
        })
        .catch(error => {
            console.log("Error posting data:", error); // Log any errors
        });

});

$("#mobile_code").intlTelInput({
    initialCountry: "in",
    separateDialCode: true,
    // utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/11.0.4/js/utils.js"
});

function maxlength(obj, wordLen) {
    var len = obj.value.split(/[\s]+/);
    if (len.length > wordLen) {
        alert("You cannot put more than " + wordLen + " words in this text area.");
    }
}

  

function test() {
    const categorySelect = document.getElementById("category");
    const currencySelect = document.getElementById("currency");
    let selectedCategory = categorySelect.value;

    if (selectedCategory === "Nurses & Paramedics (East African)") {
        currencySelect.value = "KES"; // Set to KES
        currencySelect.readOnly = true; // Disable dropdown
    } else if (selectedCategory) {
        currencySelect.value = "USD"; // Set to USD for other categories
        currencySelect.readOnly = true; // Disable dropdown
    } else {
        currencySelect.readOnly = false; // Enable selection if no category is selected
    }
    
}

