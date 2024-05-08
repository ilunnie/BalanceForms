var test1 = document.getElementById('test1');
var test2 = document.getElementById('test2');
var btn_next = document.getElementById('next');
var btn_change = document.getElementById('change');
var modal = document.getElementById('modal');

var choose1 = document.getElementById('choose1');
var choose2 = document.getElementById('choose2');
var choose3 = document.getElementById('choose3');
var choose4 = document.getElementById('choose4');
var choose5 = document.getElementById('choose5');
var choose6 = document.getElementById('choose6');
var choose7 = document.getElementById('choose7');
var choose8 = document.getElementById('choose8');
var choose9 = document.getElementById('choose9');
var choose10 = document.getElementById('choose10');

function nextValues() {
    test1.style.display = 'none';
    btn_next.style.display = 'none';
    test2.style.display = 'flex';
    btn_change.style.display = 'block';
}

function openModal() {
    var value1 = document.getElementById('value1');
    var value2 = document.getElementById('value2');
    var value3 = document.getElementById('value3');
    var value4 = document.getElementById('value4');
    var value5 = document.getElementById('value5');
    var value6 = document.getElementById('value6');
    var value7 = document.getElementById('value7');
    var value8 = document.getElementById('value8');
    var value9 = document.getElementById('value9');
    var value10 = document.getElementById('value10');

    choose1.innerHTML = value1.value == '' ? default_values[0] : value1.value;
    choose2.innerHTML = value2.value == '' ? default_values[1] : value2.value;
    choose3.innerHTML = value3.value == '' ? default_values[2] : value3.value;
    choose4.innerHTML = value4.value == '' ? default_values[3] : value4.value;
    choose5.innerHTML = value5.value == '' ? default_values[4] : value5.value
    choose6.innerHTML = value6.value == '' ? default_values[5] : value6.value;
    choose7.innerHTML = value7.value == '' ? default_values[6] : value7.value;
    choose8.innerHTML = value8.value == '' ? default_values[7] : value8.value;
    choose9.innerHTML = value9.value == '' ? default_values[8] : value9.value;
    choose10.innerHTML = value10.value == '' ? default_values[9] : value10.value;

    modal.style.display = 'flex'
}