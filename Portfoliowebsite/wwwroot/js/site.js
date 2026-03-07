
function naiveEmailCheck(email) {
    return /@.*\./.test(email);
}


function setupValidation() {
    const form = document.getElementById('contactForm');
    const hp = document.getElementById('website');
    const email = document.getElementById('Email');
    const name = document.getElementById('Name');
    const msg = document.getElementById('Message');
    const subject = document.getElementById('Subject');


    const showError = (id, message) => {
        document.getElementById(id).textContent = message;
    }

    const validateName = (value) => {
        if (!value) return 'Naam is verplicht';
        if (value.length > 100) return 'Maximaal 100 karakters toegestaan';
        return '';
    }

    const validateEmail = (value) => {
        if (!value) return 'E-mail is verplicht';
        if (value.length > 100) return 'Maximaal 100 karakters toegestaan';
        if (!naiveEmailCheck(value)) return 'Ongeldige email (naam@domein.nl)';
        return '';
    }

    const validateSubject = (value) => {
        if (!value) return 'Onderwerp is verplicht';
        if (value.length > 100) return 'Maximaal 100 karakters toegestaan';
        return '';
    }

    const validateMessage = (value) => {
        if (!value) return 'Bericht is verplicht';
        if (value.length > 1000) return 'Maximaal 1000 karakters toegestaan';
        return '';
    }

    [email, name, msg, subject].forEach(el => {
        if (el === email) {
            el.addEventListener('input', () => {
                showError('emailErr', validateEmail(el.value));
            });
        }
        if (el === name) {
            el.addEventListener('input', () => {
                showError('nameErr', validateName(el.value));
            });
        }
        if (el === subject) {
            el.addEventListener('input', () => {
                showError('subjErr', validateSubject(el.value));
            });
        }
        if (el === msg) {
            el.addEventListener('input', () => {
                showError('msgErr', validateMessage(el.value));
            });
        }
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