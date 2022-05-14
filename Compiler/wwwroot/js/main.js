// DOM Elements
const editor = document.getElementById('editor');
const editorContainer = document.getElementsByClassName('editorContainer')[0];
const numOfLines = document.getElementById('numOfLines');
const output = document.getElementById('output');
const loading = document.getElementById('loading');
const browsePopUp = document.getElementById('browsePopUp');
const hiddenOutput = document.getElementById('hiddenOutput');
const file = document.getElementById('file');
const formSubmitBtn = document.getElementById('formSubmitBtn');
const message = document.getElementById('message');

// Global Vars
let scannerData;

const getCaretPosition = () => {
    if (window.getSelection) {
        return {
            'offset': window.getSelection().anchorOffset,
            'e': window.getSelection().anchorNode,
            'range': JSON.stringify(window.getSelection().getRangeAt(0)),
        };
    }
};

const setCaretPosition = (oldCaretPosition) => {
    if (window.getSelection) {
        const selection = window.getSelection();
        const range = document.createRange();
        try {
            range.setStartAfter(editor.lastChild);
        } catch (error) {
            console.warn('::The line is empty::');
        }
        range.collapse(true);
        selection.removeAllRanges();
        selection.addRange(range);
        console.log('test', oldCaretPosition.e instanceof HTMLElement, oldCaretPosition.e);
        if (oldCaretPosition.e instanceof HTMLElement) {
            oldCaretPosition.e.focus();
        } else {
            // First Line
            editor.focus();
        }
    }
};

const reloadEditorData = () => {
    const oldCaretPosition = getCaretPosition();
    const beforeEditingHtml = editor.innerHTML;

    editor.innerHTML = editor.innerHTML.replaceAll(new RegExp(/<span style="color: #00cec9;">/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<\/span>/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(text)/, 'g'), "<span style='color: #00cec9;'>text</span>");

    if (beforeEditingHtml !== editor.innerHTML) {
        // There's a change happend
        console.log('♻️');
    }

    setCaretPosition(oldCaretPosition);
};

editor.addEventListener('input', (e) => {
    // getMainText();
    // Don't depend of length of child filter it first i.e remove the span children from the count
    /*     const length = [...editor.children].map( el => {
            console.log('NodeName: ', el.nodeName, el.nodeName != 'SPAN');
            return el.nodeName != 'SPAN'
        }).length;
        console.log('testor ', length, ' total ', editor.children.length); */
    generateLines(editor.children.length);

    reloadEditorData();

});

editor.addEventListener('keypress', (e) => {

    const key = e.code;

    if (key == 'Enter') {
        // const newElement = document.createElement('div');
        // editor.appendChild(newElement);
        const selection = window.getSelection();
        const range = document.createRange();
        range.setStartAfter(editor.lastChild);
        range.collapse(true);
        selection.removeAllRanges();
        selection.addRange(range);
        if (!(editor.lastChild instanceof HTMLElement)) {
            // Then it's the first line
            editor.focus();
        } else {
            editor.lastChild.focus();
        }
    }
});

// Foucs on editor last caret position when clicking in any place in the editor
editor.addEventListener('onclick', () => {
    editor.focus();
});

const getMainText = () => {
    return editor.innerText;
};

const generateLines = (length) => {
    if (length === 0 && numOfLines.children.length === 0) {
        // There's at least one line
        // Create New Number
        const newNum = document.createElement('div');
        newNum.className = 'number';
        newNum.innerText = Number(numOfLines.children.length) + 1;
        // Append to the dom
        numOfLines.appendChild(newNum);
    }
    while (numOfLines.children.length <= length) {
        // Create New Number
        const newNum = document.createElement('div');
        newNum.className = 'number';
        newNum.innerText = Number(numOfLines.children.length) + 1;

        // Append to the dom
        numOfLines.appendChild(newNum);
    }

    if (numOfLines.children.length > length) {
        while (numOfLines.children.length > length + 1) {
            // remove to the dom
            numOfLines.removeChild(numOfLines.children[numOfLines.children.length - 1]);
        }
    }
};

// Cancel the default paste and paste it as a text
editor.addEventListener('paste', (e) => {
    e.preventDefault();
    const text = e.clipboardData.getData('text/plain');
    document.execCommand('insertHTML', false, text);
    reloadEditorData();
});

// Toolbar Buttons 

const comment = () => {

};

const unComment = () => {

};

const scan = () => {
    // Set Loading true
    displayLoading();

    // Send Post Req to Scanner
    const code = getMainText();
    const formData = new FormData();
    formData.append('code', code);

    console.log('Main Data', code);
    fetch(`${location.origin}/Scanner`, {
        method: 'POST',
        body: formData,
    }).then(res => res.json())
        .then(res => {
            // Stop the loading
            hideLoading();
            if (res.status == 200) {
                scannerData = res.data;
                // remove old elements
                output.innerHTML = '<h2>Comiler : </h2>';
                // Display output and it's data
                res.data.forEach(el => {
                    let newElement = document.createElement('div');
                    newElement.innerText = el;
                    output.appendChild(newElement);
                });
                output.style.transform = 'translateY(0)';
                console.log(scannerData);
            } else {
                // Display error message
                displayMessage(`There's an error while scanning your code, please try again`);
            }
        }).catch(e => {
            // Stop the loading
            hideLoading();
            // Display error message
            displayMessage(`There's an error while scanning your code, please try again`);
        });

};

const parse = () => {
    // Set Loading true
    displayLoading();

    // Send Post Req to Parser 
    console.log('Main Data', scannerData);

    // Set Loading false
    hideLoading();
};

const browse = () => {
    // Click to choose file
    file.click();
};

const uploadFile = () => {
    // Submit form to upload it
    formSubmitBtn.click();
};

const formSubmit = (e) => {
    e.preventDefault();

    // Set Loading true
    displayLoading();

    const uploadedFile = file.files[0];

    if (uploadedFile) {
        // Hide loading
        hideLoading();
        // Display error message
        displayMessage(`You must choose a file!`);
        return;
    }

    // checking the file 
    // Like file ext ..

    // Submit to the backend
    const formData = new FormData();
    formData.append('file', uploadFile);

    fetch('backendUrl', {
        method: 'Post',
        data: formData
    }).then(res => res.json())
        .then(data => {
            // Stop the loading
            hideLoading();
            if (data.status == 200) {
                // Display PopUp with success message
                browsePopUp.children[0].children[0].innerText = "Your file is uploaded successfully!";
                browsePopUp.style.transform = 'scale(1)';
            } else {
                // Display error message
                displayMessage(`There's an error while uploading your file, please try again`);
            }
        }).catch(e => {
            // Stop the loading
            hideLoading();
            // Display error message
            displayMessage(`There's an error while uploading your file, please try again`);
        });

};

// Hidden File Buttons ( Browse )
const scanHiddenFile = () => {
    // Set Loading true
    displayLoading();

    // Send Post Req to Scanner
    const data = getMainText();
    console.log('Main Data', data);
    scannerData = null;

    // Set Loading false
    hideLoading();
};

const parseHiddenFile = () => {
    // Set Loading true
    displayLoading()

    // Send Post Req to Parser 
    console.log('Main Data', scannerData);

    // Set Loading false
    hideLoading();
};

const displayLoading = () => {
    loading.style.transform = `scaleZ(1)`;
};

const hideLoading = () => {
    loading.style.transform = `scaleZ(0)`;
};

const displayMessage = (messageText) => {
    message.innerText = messageText;
    message.style.transform = `translate(-50%, 0vh)`;

    // Hide message after 5s
    setTimeout(() => {
        message.style.transform = `translate(-50%, -30vh)`;
    }, 5000);
};