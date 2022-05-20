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

const reloadEditorData = () => {
    let offset = Cursor.getCurrentCursorPosition(editor);

    //const oldCaretPosition = getCaretPosition();

    const beforeEditingHtml = editor.innerHTML;

    // Remove all spans and styles 
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<span (.*?)>/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<\/span>/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/style="(.*?)"/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/color="(.*?)"/, 'g'), '');

    // Init keywords colors

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(If)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightPurple);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Else)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightPurple);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Include)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkGreen);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Loopwhen)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkCyan);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Iteratewhen)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkCyan);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Turnback)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightRed);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Stop)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightRed);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Iow)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(SIow)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Chlo)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Chain)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Iowf)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(SIowf)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Worthless)(\&nbsp\;| ))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/((\&nbsp\;| |)(Loli)(\&nbsp\;| |))/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkBlue);'>${c.trim()}</span>`));

    let editorHTML = editor.innerHTML.split('<div>');
    var start = false;

    for (let i = 0; i < editorHTML.length; i++) {

        console.log('test', editorHTML[i]);

        // Single Line Comment
        if (editorHTML[i].includes('$$$')) {
            let startIndex = editorHTML[i].indexOf('$$$');
            // Remove any other spans/colors in the commented text
            let relpacedHTML = editorHTML[i].substring(startIndex).replace(new RegExp(/style="(.*?)">/, 'g'), '');
            // Add comment style
            editorHTML[i] = `${editorHTML[i].substring(0, startIndex)}<span style='color: var(--commentColor);'>${relpacedHTML}</span>`;
        }

        if (editorHTML[i].indexOf('/$') != -1) {
            start = true;
        }

        if (start) {

            // Remove any other spans/colors in the commented text
            editorHTML[i] = editorHTML[i].replace(new RegExp(/<span (.*?)>/, 'g'), '');
            editorHTML[i] = editorHTML[i].replace(new RegExp(/<\/span>/, 'g'), '');

            if (editorHTML[i].indexOf('/$') != -1 && editorHTML[i].indexOf('$/') != -1) {
                // Same line
                let startIndex = editorHTML[i].indexOf('/$');
                let endIndex = editorHTML[i].indexOf('$/') + 2;
                editorHTML[i] = `${editorHTML[i].substring(0, startIndex)}<span style='color: var(--commentColor);'>${editorHTML[i].substring(startIndex, endIndex)}</span>${editorHTML[i].substring(endIndex)}`;
            } else if (editorHTML[i].indexOf('/$') != -1) {
                // Only start in on this line
                let startIndex = editorHTML[i].indexOf('/$');
                editorHTML[i] = `${editorHTML[i].substring(0, startIndex)}<span style='color: var(--commentColor);'>${editorHTML[i].substring(startIndex)}</span>`;
            } else if (editorHTML[i].indexOf('$/') != -1) {
                // only end is on this line
                let endIndex = editorHTML[i].indexOf('$/') + 2;
                editorHTML[i] = `<span style='color: var(--commentColor);'>${editorHTML[i].substring(0, endIndex)}</span>${editorHTML[i].substring(endIndex)}`;
            } else {
                // the line is commented completely
                editorHTML[i] = `<span style='color: var(--commentColor);'>${editorHTML[i]}</span>`;
            }

        }

        if (editorHTML[i].indexOf('$/') != -1) {
            start = false;
        }
    }

    editor.innerHTML = editorHTML.join('<div>');


    if (beforeEditingHtml !== editor.innerHTML) {
        // There's a change happend
        console.log('♻️');
    }

    Cursor.setCurrentCursorPosition(offset, editor);
    editor.focus();
    //setCaretPosition(oldCaretPosition);
};

editor.addEventListener('input', (e) => {
    generateLines();
    reloadEditorData();
});

editor.addEventListener('keypress', (e) => {

    const key = e.key;

    //console.log(e);

    if (key == '\n') {
        // Auto Complete
        console.log('complete it !');
    }

    if (key == 'Enter') {
        /* const newElement = document.createElement('div');
        editor.appendChild(newElement); */
        /* const selection = window.getSelection();
        const range = document.createRange();
        range.setStartAfter(editor.lastChild, 0);
        range.collapse(true);
        selection.removeAllRanges();
        selection.addRange(range);
        editor.focus(); */
    }
});

// Foucs on editor last caret position when clicking in any place in the editor
editor.addEventListener('onclick', () => {
    editor.focus();
});

const getMainText = () => {
    return editor.innerText;
};

const generateLines = () => {
    let length = 0;
    // To fix lines on paste , count the divs inside other divs
    [...editor.children].forEach(el => {
        if (el.tagName != 'SPAN') {
            length++;
        }
    });

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
    /* e.preventDefault();
    const text = e.clipboardData.getData('text/plain');
    document.execCommand('insertHTML', false, text); */
    reloadEditorData();
    generateLines();
});

// Functions

const closeOutput = () => {
    output.style.display = 'none';
}

// Toolbar Buttons 

const comment = () => {
    const range = window.getSelection().getRangeAt(0);
    let offset = Cursor.getCurrentCursorPosition(editor);

    if (range) {
        console.log(range, window.getSelection().rangeCount);
        const startContainerText = range.startContainer.nodeValue.trim();
        const endContainerText = range.endContainer.nodeValue.trim();

        const startPoint = range.startOffset;
        const endPoint = range.endOffset;

        // On the same line
        if (startContainerText == endContainerText) {
            const textToReplace = startContainerText.substring(startPoint, endPoint).trim();

            for (let i = 0; i < editor.childNodes.length; i++) {
                let child = editor.childNodes[i];
                if (!child) {
                    continue;
                }

                if (child.nodeValue) {
                    if (editor.childNodes[i].nodeValue.includes(startContainerText)) {
                        editor.childNodes[i].nodeValue = editor.childNodes[i].nodeValue.replace(`${textToReplace}`, `/$ ${textToReplace} $/`);
                        break;
                    }
                } else if (child.innerText) {
                    if (editor.innerText[i].nodeValue.includes(startContainerText)) {
                        editor.innerText[i].nodeValue = editor.innerText[i].nodeValue.replace(`${textToReplace}`, `/$ ${textToReplace} $/`);
                        break;
                    }
                }

            }

            Cursor.setCurrentCursorPosition(offset, editor);
            editor.focus();
            reloadEditorData();
            return;
        }

        // On multi lines
        const startTextToReplace = startContainerText.substring(startPoint).trim();
        const endTextToReplace = endContainerText.substring(0, endPoint).trim();

        console.log('Text ', startTextToReplace, endTextToReplace)

        for (let i = 0; i < editor.childNodes.length; i++) {
            let child = editor.childNodes[i];
            console.log(child.nodeValue || child.innerText)
            if (!child) {
                continue;
            }

            if (child.nodeValue) {// Comment Open Tag
                if (child.nodeValue.includes(startTextToReplace)) {
                    console.log('Test Start')
                    child.nodeValue = ` /$ ${child.nodeValue}`;
                }

                // Comment Close Tag
                if (child.nodeValue.includes(endTextToReplace)) {
                    console.log('Test End')
                    child.nodeValue = `${child.nodeValue} $/ `;
                    break;
                }
            } else if (child.innerText) {
                // Comment Open Tag
                if (child.innerText.includes(startTextToReplace)) {
                    console.log('Test Start')
                    child.innerText = ` /$ ${child.innerText}`;
                }

                // Comment Close Tag
                if (child.innerText.includes(endTextToReplace)) {
                    console.log('Test End')
                    child.innerText = `${child.innerText} $/ `;
                    break;
                }
            }

        }
    }

    Cursor.setCurrentCursorPosition(offset, editor);
    editor.focus();

    reloadEditorData();

    // editor.innerHTML = editor.innerHTML.replace(document.getSelection().focusNode.data, newTd);
    // document.execCommand('bold', false, newTd);
};

const unComment = () => {
    const range = window.getSelection().getRangeAt(0);
    let offset = Cursor.getCurrentCursorPosition(editor);
    if (range) {
        console.log(range, window.getSelection().rangeCount);
        const startContainerText = range.startContainer.nodeValue.trim();
        const endContainerText = range.endContainer.nodeValue.trim();

        const startPoint = range.startOffset;
        const endPoint = range.endOffset;

        let editorText = editor.innerText.split('\n');

        for (let i = 0; i < editorText.length; i++) {
            // On the same line
            if (startContainerText == endContainerText && editorText[i].includes(startContainerText) && editorText[i].includes(endContainerText)) {
                editorText[i] = editorText[i].replace(new RegExp(/\/\$/, 'g'), '');
                editorText[i] = editorText[i].replace(new RegExp(/\$\//, 'g'), '');
                break;
            }

            // Comment Open Tag
            if (editorText[i].includes(startContainerText)) {
                editorText[i] = editorText[i].replace(new RegExp(/\/\$/, 'g'), '');
            }

            // Comment Close Tag
            if (editorText[i].includes(endContainerText)) {
                editorText[i] = editorText[i].replace(new RegExp(/\$\//, 'g'), '');
                break;
            }
        }

        editor.innerText = editorText.join('\n');
    }

    Cursor.setCurrentCursorPosition(offset, editor);
    editor.focus();

    reloadEditorData();
};

const scan = () => {
    // Set Loading true
    displayLoading();

    const code = (getMainText().replace(new RegExp(/(&nbsp;|&#160;| )/, 'g'), ' ')).replace(new RegExp(/(\n)/, 'g'), ' \n '); // The code that will be sent to the scanner : add space to the end

    // Check the text
    if (code == '' || code == null) {
        // Display error message
        displayMessage(`There's an error! Please write a code to scan!`);

        hideLoading();
        return;
    }

    // Send Post Req to Scanner
    const formData = new FormData();
    formData.append('code', `${code} `);

    console.log("Scanner data ", `${code} `);

    fetch(`${location.origin}/Scanner`, {
        method: 'POST',
        body: formData,
    }).then(res => res.json())
        .then(res => {
            // Stop the loading
            hideLoading();
            if (res.status == 200) {
                scannerData = res.tokens;
                console.log(res);
                // remove old elements
                output.innerHTML = '<h2>Comiler : </h2><button onclick="closeOutput()">Close</button>';
                // Display output and it's data
                res.output.forEach(el => {
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

    // No scanner data
    if (!scannerData) {
        // Display error message
        displayMessage(`There's an error! Please scan your code first!`);

        hideLoading();
        return;
    }

    // Send Post Req to Parser 
    console.log('Parser Data', scannerData);

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

    if (!uploadedFile) {
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
    formData.append('data', uploadFile);
    formData.append('s', "Test");

    fetch(`${location.origin}/scanner/ScanHiddenFile`, {
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


// Credit to Liam (Stack Overflow)
// https://stackoverflow.com/a/41034697/3480193
class Cursor {
    static getCurrentCursorPosition(parentElement) {
        var selection = window.getSelection(),
            charCount = -1,
            node;

        if (selection.focusNode) {
            if (Cursor._isChildOf(selection.focusNode, parentElement)) {
                node = selection.focusNode;
                charCount = selection.focusOffset;

                while (node) {
                    if (node === parentElement) {
                        break;
                    }

                    if (node.previousSibling) {
                        node = node.previousSibling;
                        charCount += node.textContent.length;
                    } else {
                        node = node.parentNode;
                        if (node === null) {
                            break;
                        }
                    }
                }
            }
        }
        return charCount;
    }

    static setCurrentCursorPosition(chars, element) {
        if (chars >= 0) {
            var selection = window.getSelection();

            let range = Cursor._createRange(element, { count: chars });

            if (range) {
                range.collapse(false);
                selection.removeAllRanges();
                selection.addRange(range);
            }
        }
    }

    static _createRange(node, chars, range) {
        if (!range) {
            range = document.createRange()
            range.selectNode(node);
            range.setStart(node, 0);
        }

        if (chars.count === 0) {
            range.setEnd(node, chars.count);
        } else if (node && chars.count > 0) {
            if (node.nodeType === Node.TEXT_NODE) {
                if (node.textContent.length < chars.count) {
                    chars.count -= node.textContent.length;
                } else {
                    range.setEnd(node, chars.count);
                    chars.count = 0;
                }
            } else {
                for (var lp = 0; lp < node.childNodes.length; lp++) {
                    range = Cursor._createRange(node.childNodes[lp], chars, range);

                    if (chars.count === 0) {
                        break;
                    }
                }
            }
        }

        return range;
    }

    static _isChildOf(node, parentElement) {
        while (node !== null) {
            if (node === parentElement) {
                return true;
            }
            node = node.parentNode;
        }

        return false;
    }
}