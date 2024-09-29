async function submitLogin(event) {
    event.preventDefault();

    try {
        const email = document.getElementById('exampleInputEmail1').value;
        const password = document.getElementById('exampleInputPassword1').value;

        const response = await fetch('ApiLogin/Login', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password }),
        });


        const result = await response.json();

        if (response.ok) {
            localStorage.setItem('jwtToken', result.data.token);
            window.location.href = 'Home/Index';
        } else {
            alert(result.message || 'Login failed');
        }
    } catch (error) {
        //alert('Error message: ' + error.message);
        console.log(error.message);
    }
}
