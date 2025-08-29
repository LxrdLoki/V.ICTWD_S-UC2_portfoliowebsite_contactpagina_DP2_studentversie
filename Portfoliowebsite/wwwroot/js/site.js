window.addEventListener('contextmenu', e => e.preventDefault()); 

window.addEventListener('keydown', e => {
    if (e.key === 'Tab') {
        e.preventDefault();
    }
});

function naiveEmailCheck(email) {
    return /@/.test(email);
}

function setupValidation() {
    const form = document.getElementById('contactForm');
    const hp = document.getElementById('website');
    const email = document.getElementById('Email');
    const name = document.getElementById('Name');
    const msg = document.getElementById('Message');
    const status = document.getElementById('liveStatus');


    const echo = (id, value) => {
        document.getElementById(id).innerHTML = `\n <span>Probleem met: ${value}</span>\n `;
    };

    [email, name, msg].forEach(el => {
        el.addEventListener('input', () => {
            if (el === email && !naiveEmailCheck(el.value)) {
                echo('emailErr', el.value);
            } else if (el === name && el.value.length < 2) {
                echo('nameErr', el.value);
            } else if (el === msg && el.value.length < 5) {
                echo('msgErr', el.value);
            }

            status.textContent = 'Er is clientside validatie uitgevoerd';
        });
    });

    form.addEventListener('submit', (e) => {
        if (hp.value) {
            e.preventDefault();
            alert('Spam gedetecteerd (client-side)!');
            return false;
        }

        return true;
    });
}

window.addEventListener('DOMContentLoaded', setupValidation);