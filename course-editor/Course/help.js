/*function textBoxFocus(textBox){
    if (textBox.value == textBox.defValue){
        textBox.value = '';
        textBox.style.fontStyle = 'normal';
    }
}

function textBoxBlur(textBox){
    if (textBox.value == ''){
        textBox.value =  textBox.defValue;
        textBox.style.fontStyle = 'italic';
    }
}

function textBoxInit(textBox, defValue){
    textBox.defValue = defValue;
    textBoxBlur(textBox);
}*/

function disableSelect(e) {
    return false;
}

function reEnable() {
    return true
}

document.onselectstart = new Function("return false");
document.oncontextmenu = new Function("return false");
/*if (window.sidebar) {
    document.onmousedown = disableSelect;
    document.onclick = reEnable;
}*/