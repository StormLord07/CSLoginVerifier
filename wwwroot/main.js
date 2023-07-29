let btn = document.getElementById('submit');
let URL = 'http://localhost:5000/login'
document.getElementById('result-value').value = false.toString();


btn.onclick = (e) => {
    let req = new XMLHttpRequest();
    let resultElement = document.getElementById('result-value');

    req.open("POST", URL, true);

    req.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    req.onreadystatechange = function () {
        if (req.readyState == XMLHttpRequest.DONE && req.status == 200) {
            let answer = JSON.parse(req.response);
            if (typeof (answer.access) != 'undefined') {

                resultElement.value = answer.access.toString();
                // Remove both classes in case they were added in a previous request
                resultElement.classList.remove('valid', 'invalid');

                // Add the correct class based on the response
                if (answer.access) {
                    resultElement.classList.add('valid');
                } else {
                    resultElement.classList.add('invalid');
                }
            }
        }
    }
    req.send(JSON.stringify({
        Login: document.getElementById('login').value,
        Password: document.getElementById('pass').value
    }));
};