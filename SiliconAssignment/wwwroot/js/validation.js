const formErrorHandler = (element, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validationResult) {
        element.classList.remove('input-validation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
        spanElement.innerHTML = ''
    }
    else {
        element.classList.add('input-validation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        spanElement.innerHTML = element.dataset.valRequired
    }
}

const compareValidator = (element, compareWithValue) => {
    if (element.value === compareWithValue)
        return true

    return false
}


const textValidator = (element, minLength = 2) => {
    if (element.value.length >= minLength)
        formErrorHandler(element, true)

    else { 
    formErrorHandler(element, false)
    }
}

const checkboxValidator = (element) => {
    if (element.checked)
        formErrorHandler(element, true)

    else {
        formErrorHandler(element, false)
    }
}


let forms = document.querySelectorAll('form')
let input = forms[0].querySelectorAll('input')

input.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidator(e.target)
            })
        }
        else {
            input.addEventListener('keyup', (e) => {

                switch (e.target.type) {
                    case 'text':
                        textValidator(e.target)
                        break;

                    case 'email':
                        textValidator(e.target)
                        break;
                }
            })
        }


    }
})