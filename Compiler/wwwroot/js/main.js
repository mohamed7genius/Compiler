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
const autoCompleteContainer = document.getElementById('autoCompleteContainer');

// Global Vars
let scannerData;
let hiddenFileData;
let scannerHiddenFile;
let navigationData;
let errors = {};
const keywords = ['If', 'Else', 'Include', 'Loopwhen', 'Iteratewhen', 'Turnback', 'Stop', 'Iow', 'SIow', 'Chlo', 'Chain', 'Iowf',
                    'SIowf', 'Worthless', 'Loli'];

const removeAllStyles = () => {
    // Remove all spans and styles 
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<span (.*?)>/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<a (.*?)>/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(/&gt;/g, ">");
    console.log('a before ', editor.innerHTML);
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<\/span>/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<\/a>/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/style="(.*?)"/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/color="(.*?)"/, 'g'), '');
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/<br>/, 'g'), ' \&nbsp\;');
}

const goToElement = (e) => {
    const elementText = e.target.innerText;
    const firstelement = editor.innerText.indexOf(elementText);
    const elements = document.getElementsByTagName('a');
    console.log('total elements ', elements);
    [...elements].forEach(el => {
        if (el.innerText == elementText) {
            el.focus();
            Cursor.setCurrentCursorPosition(firstelement +elementText.length, editor);
        }
    });
}

const reloadEditorData = (initOffset) => {
    let offset = initOffset ? initOffset : Cursor.getCurrentCursorPosition(editor);
    console.log('new offset', offset);

    //const oldCaretPosition = getCaretPosition();

    const beforeEditingHtml = editor.innerHTML;

    // Remove all spans and styles 
    removeAllStyles();

    // Add navigation to ids
    if (navigationData) {
        console.log('before loop', navigationData, Object.keys(navigationData));
        Object.keys(navigationData).forEach(obj => {
            console.log('obj', obj);
            // same file
            if (!navigationData[obj]) {
                // editor.innerHTML = editor.innerHTML.replaceAll(String(obj), `<a onclick='${goToElement(obj)}'>${String(obj)}</a>`);
                editor.innerHTML = editor.innerHTML.replaceAll(obj, "<a onclick='goToElement(event)'>" + obj + "</a>");
                // console.log('test first', editor.innerHTML.replaceAll(String(obj), `<a onclick="goToElement('${obj}')">${String(obj)}</a>`))
            } else {
                // another file
                // Nav to include link or open it in pop up
            }
        })
    }

    // Init errors 
    if (errors) {
        Object.keys(errors).forEach(err => {
            console.log('Err', errors[err]);
            // editor.innerHTML = editor.innerHTML.substring(0, errors[err].start) + `<span title='error!' class='errorLine'>${errors[err.name]}</span>` + editor.innerHTML.substring(errors[err].end);
            editor.innerHTML = editor.innerHTML.replace(String(errors[err].Name), `<span title='error!' class='errorLine'>${String(errors[err].Name) }</span>`);

        })
    }

    // Init keywords colors
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(If)/, 'g'), (c) => c.replace(c.trim(), `<span title='If statement' style='color: var(--lightPurple);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Else)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightPurple);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Include)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkGreen);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Loopwhen)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkCyan);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Iteratewhen)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkCyan);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Turnback)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightRed);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Stop)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightRed);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Iow)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(SIow)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Chlo)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Chain)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Iowf)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(SIowf)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));
    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Worthless)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--lightBlue);'>${c.trim()}</span>`));

    editor.innerHTML = editor.innerHTML.replace(new RegExp(/(Loli)/, 'g'), (c) => c.replace(c.trim(), `<span style='color: var(--darkBlue);'>${c.trim()}</span>`));

    let editorHTML = editor.innerHTML.split('<div>');
    var start = false;

    for (let i = 0; i < editorHTML.length; i++) {


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
    generateLines();
};

editor.addEventListener('input', (e) => {
    generateLines();
    reloadEditorData();
});

const printingAutoComplete = (el, offset, nodeThatWrittenIn, textToComplete) => {

    // Remove all spans and styles 
    removeAllStyles();

    const editorHTML = editor.innerHTML.split('</div>');
    for (let i = 0; i < editorHTML.length; i++){
        if (editorHTML[i].includes(nodeThatWrittenIn)) {
            const startIndex = editorHTML[i].lastIndexOf(textToComplete);
            const endIndex = startIndex + textToComplete.length;
            editorHTML[i] = editorHTML[i].substring(0, startIndex) + el + editorHTML[i].substring(endIndex);
        }
    }

    editor.innerHTML = editorHTML.join('</div>');

    // Set caret posistion to the end of the word
    Cursor.setCurrentCursorPosition(Number(offset) + el.length - textToComplete.length, editor);

    // Hide Pop Up
    autoCompleteContainer.style.transform = 'scaleZ(0)';

    // Reload data colors
    reloadEditorData();
}

editor.addEventListener('keypress', async (e) => {

    const key = e.key;

    // console.log(e);

    // Auto Complete Ctrl + Enter
    if (key == '\n') {

        scan();

        // Fetch the data
        const res = await fetch(`${location.origin}/Editor/GetAutoCompleteID`, { method: 'GET' });
        const data = await res.json();
      
        if (data.status == 200) {
            navigationData = data.data;
            const autoCompleteData = [...Object.keys(data.data)].concat(keywords);
            console.log(autoCompleteData);
            // Get the text to complete
            const offset = Cursor.getCurrentCursorPosition(editor);
            const nodeText = window.getSelection().getRangeAt(0).startContainer.nodeValue;
            if (!nodeText || nodeText == '') {
                return;
            }
            const nodeThatWrittenIn = nodeText.trim();
            const textToComplete = nodeThatWrittenIn.substring(nodeThatWrittenIn.lastIndexOf(' ') + 1, offset);

            // Pop up to select from
            autoCompleteContainer.innerHTML = '';
            autoCompleteData.forEach(el => {
                if (el.includes(textToComplete)) {
                    let newElement = document.createElement('li');
                    newElement.innerText = el;
                    newElement.setAttribute('onclick', `printingAutoComplete('${el}', '${offset}', '${nodeThatWrittenIn}', '${textToComplete}')`);
                    autoCompleteContainer.appendChild(newElement);
                }
            });

            // There's something to complete the text with
            if (autoCompleteContainer.children.length > 0) {
                autoCompleteContainer.style.transform = 'scale3d(1,1,1)';
            }

        } else {
            // Display error message
            displayMessage(`There's an error! Please try again later!`);
        }
    }

    if (key == 'Enter') {
        // Wait 25 ms untill the node init then move the cursor to the new position
        setTimeout(() => {
            Cursor.setCurrentCursorPosition(Cursor.getCurrentCursorPosition(editor) + 1, editor);
        }, 25);
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

    let spanLength = 0;

    // To fix lines on paste , count the span which contains the colors (extra child number)
    [...editor.children].forEach(el => {
        if (el.tagName == 'SPAN') {
            spanLength++;
        }
    });

    const length = editor.childElementCount + 1 - spanLength;

    if (numOfLines.children.length != length) {
        // Remove old children
        numOfLines.innerHTML = '';
        for (let i = 0; i < length; i++) {
            // Create new child number
            const newNum = document.createElement('div');
            newNum.className = 'number';
            newNum.innerText = Number(numOfLines.children.length) + 1;
            // Append to the dom
            numOfLines.appendChild(newNum);
        }
    }

};

// On user code/text paste
editor.addEventListener('paste', (e) => {
    reloadEditorData();
    generateLines();
});

// Functions
const closeOutput = () => {
    output.style.transform = 'translateY(200vh)';
    editor.style.marginBottom = '';
}

// Toolbar Buttons 
const comment = () => {
    const range = window.getSelection().getRangeAt(0);
    let offset = Cursor.getCurrentCursorPosition(editor);

    if (range) {
        console.log(range, window.getSelection().rangeCount);
        const startContainerText = range.startContainer.nodeValue;
        const endContainerText = range.endContainer.nodeValue;

        const startPoint = range.startOffset;
        const endPoint = range.endOffset;

        // On the same line
        if (startContainerText.trim() == endContainerText.trim()) {
            const textToReplace = startContainerText.trim().substring(startPoint, endPoint).trim();

            for (let i = 0; i < editor.childNodes.length; i++) {
                let child = editor.childNodes[i];
                if (!child) {
                    continue;
                }

                if (child.nodeValue) {
                    if (editor.childNodes[i].nodeValue.includes(startContainerText.trim())) {
                        editor.childNodes[i].nodeValue = editor.childNodes[i].nodeValue.replace(`${textToReplace}`, `/$ ${textToReplace} $/`);
                        break;
                    }
                } else if (child.innerText) {
                    if (editor.childNodes[i].innerText.includes(startContainerText.trim())) {
                        editor.childNodes[i].innerText = editor.childNodes[i].innerText.replace(`${textToReplace}`, `/$ ${textToReplace} $/`);
                        break;
                    }
                }

            }

            reloadEditorData(offset+4);
            return;
        }

        // On multi lines
        const startTextToReplace = startContainerText.substring(startPoint);
        const endTextToReplace = endContainerText.substring(0, endPoint);

        console.log('Text ', startTextToReplace, endTextToReplace)

        for (let i = 0; i < editor.childNodes.length; i++) {
            let child = editor.childNodes[i];
            console.log(child.nodeValue || child.innerText)
            if (!child) {
                continue;
            }

            if (child.nodeValue) {// Comment Open Tag
                if (child.nodeValue.includes(startTextToReplace.trim())) {
                    child.nodeValue = `${child.nodeValue.substring(0, startPoint)} /$ ${child.nodeValue.substring(startPoint)}`;
                }

                // Comment Close Tag
                if (child.nodeValue.includes(endTextToReplace.trim())) {
                    child.nodeValue = `${child.nodeValue.substring(0, endPoint)} $/ ${child.nodeValue.substring(endPoint)}`;
                    break;
                }
            } else if (child.innerText) {
                // Comment Open Tag
                if (child.innerText.includes(startTextToReplace.trim())) {
                    child.innerText = `${child.innerText.substring(0, startPoint)} /$ ${child.innerText.substring(startPoint)}`;
                }

                // Comment Close Tag
                if (child.innerText.includes(endTextToReplace.trim())) {
                    child.innerText = `${child.innerText.substring(0, endPoint)} $/ ${child.innerText.substring(endPoint)}`;
                    break;
                }
            }

        }
    }

    Cursor.setCurrentCursorPosition(offset, editor);
    editor.focus();

    reloadEditorData();
};

const unComment = () => {
    const range = window.getSelection().getRangeAt(0);
    let offset = Cursor.getCurrentCursorPosition(editor);
    if (range) {
        console.log(range, window.getSelection().rangeCount);
        const startContainerText = range.startContainer.nodeValue;
        const endContainerText = range.endContainer.nodeValue;

        const startPoint = range.startOffset;
        const endPoint = range.endOffset - 2;

        let editorHTML = editor.innerHTML.split('</div>');
        let editorText = editor.innerText.split('\n');
        console.log('before', editorHTML);

        // Single line comment
        if (startContainerText.trim() == endContainerText.trim()) {
            for (let i = 0; i < editorText.length; i++) {
                let textToCompare = editorText[i].replace(new RegExp(/<span (.*?)>/, 'g'), '');
                textToCompare = textToCompare.replace(new RegExp(/<\/span>/, 'g'), '');
                if (textToCompare.includes(startContainerText.trim()) && textToCompare.includes(endContainerText.trim())) {
                    console.log('entered before', editorText[i])
                    editorText[i] = editorText[i].replace('/$', '');
                    editorText[i] = editorText[i].replace('$/', '');
                    console.log('entered after', editorText[i])
                    break;
                }
            }

            editor.innerText = editorText.join('\n');
            reloadEditorData(offset - 4);

            return;
        }


        // Mulit line comment

        for (let i = 0; i < editorHTML.length; i++) {

            let htmlToCompare = editorText[i].replace(new RegExp(/<span (.*?)>/, 'g'), '');
            htmlToCompare = htmlToCompare.replace(new RegExp(/<\/span>/, 'g'), '');

            console.log('testing ', htmlToCompare.includes(startContainerText.trim()), htmlToCompare.includes(endContainerText.trim()));
            // Comment Open Tag
            if (htmlToCompare.includes(startContainerText.trim())) {
                editorHTML[i] = editorHTML[i].replace('/$', '');
            }

            console.log('close tag', htmlToCompare.nodeText, endPoint);

            // Comment Close Tag
            if (htmlToCompare.includes(endContainerText.trim()) || htmlToCompare.indexOf('$/') == endPoint ) {
                console.log('before ', editorHTML[i]);
                editorHTML[i] = editorHTML[i].replace('$\/', '');
                console.log('after ', editorHTML[i]);
                break;
            }
        }

        editor.innerHTML = editorHTML.join('</div>');
    }

    Cursor.setCurrentCursorPosition(offset, editor);
    editor.focus();

    reloadEditorData();
};

const scan = () => {
    // Set Loading true
    displayLoading();

    const code = getMainText(); // The code that will be sent to the scanner : add space to the end

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
                // Loop throught errors
                errors = res.errors;
                reloadEditorData();
                // remove old elements
                output.innerHTML = '<h2>Compiler : </h2><button onclick="closeOutput()">Close</button>';
                // Display output and it's data
                res.output.forEach(el => {
                    let newElement = document.createElement('div');
                    newElement.innerText = el;
                    output.appendChild(newElement);
                });
                output.style.transform = 'translateY(0)';
                editor.style.marginBottom = '33vh';
                console.log(scannerData);
            } else {
                // Display error message
                displayMessage(`There's an error while scanning your code, please try again`);
            }
        }).catch(e => {
            // Stop the loading
            hideLoading();
            // Display error message
            displayMessage(`There's an error while scanning your code, please try again ${e}`);
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
    const formData = new FormData();
    formData.append('code', scannerData.join(" "));

    console.log('Parser Data', scannerData.join(" "));

    fetch(`${location.origin}/Parser`, {
        method: 'POST',
        body: formData,
    }).then(res => res.json())
        .then(res => {
            // Stop the loading
            hideLoading();
            if (res.status == 200) {
                parserData = res.data;
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
                displayMessage(`There's an error while parsing your code, please try again`);
            }
        }).catch(e => {
            // Stop the loading
            hideLoading();
            // Display error message
            displayMessage(`There's an error while parsing your code, please try again`);
        });

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

    if (!file.files[0]) {
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
    formData.append('file', file.files[0]);

    fetch(`${location.origin}/File/ReadHiddenFile`, {
        method: 'Post',
        body: formData
    }).then(res => res.json())
        .then(res => {
            // Stop the loading
            hideLoading();
            console.log('res', res, res.status == 400);
            if (res.status == 200) {
                // Display PopUp with success message
                hiddenFileData = res.data;
                browsePopUp.children[0].children[0].innerText = "Your file is uploaded successfully!";
                browsePopUp.style.transform = 'scale3d(1,1,1)';
            } else if (res.status == 400) {
                // Display error message
                displayMessage(`There's an error! The file cann't be empty!`);
            } else {
                // Display error message
                displayMessage(`There's an error while uploading your file, please try again`);
            }
        }).catch(e => {
            // Stop the loading
            hideLoading();
            // Display error message
            displayMessage(`There's an error while uploading your file, please try again ${e}`);
        });

};

// Hidden File Buttons ( Browse )
const scanHiddenFile = () => {
    // Set Loading true
    displayLoading();

    // Send Post Req to Scanner
    const formData = new FormData();
    formData.append('code', hiddenFileData);
    formData.append('filePath', "hidden");

    console.log("Scanner data ", `${hiddenFileData} `);

    fetch(`${location.origin}/Scanner`, {
        method: 'POST',
        body: formData,
    }).then(res => res.json())
        .then(res => {
            // Stop the loading
            hideLoading();
            if (res.status == 200) {
                scannerHiddenFile = res.tokens;
                console.log(res);
                // remove old elements
                hiddenOutput.innerHTML = '<h2>Compiler : </h2>';
                // Display output and it's data
                res.output.forEach(el => {
                    let newElement = document.createElement('div');
                    newElement.innerText = el;
                    hiddenOutput.appendChild(newElement);
                });
                hiddenOutput.style.display = 'block';
                console.log(scannerData);
            }else {
                // Display error message
                displayMessage(`There's an error while scanning your file, please try again`);
            }
        }).catch(e => {
            // Stop the loading
            hideLoading();
            // Display error message
            displayMessage(`There's an error while scanning your code, please try again ${e}`);
        });
};

const parseHiddenFile = () => {
    // Set Loading true
    displayLoading();

    // No scanner data
    if (!scannerHiddenFile) {
        // Display error message
        displayMessage(`There's an error! Please scan your file first!`);

        hideLoading();
        return;
    }

    // Send Post Req to Parser 
    console.log('Parser Data', scannerHiddenFile);

    // Set Loading false
    hideLoading();
};

const closeHiddenFile = () => {
    browsePopUp.style.transform = 'scale3d(0,0,0)';
    hiddenOutput.style.display = 'none';
}

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