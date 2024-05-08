var test1 = document.getElementById('test1');
var test2 = document.getElementById('test2');
var btn_next = document.getElementById('next');
var btn_change = document.getElementById('change');
var form = document.getElementById('form');

function nextValues() {
    if (confirm("Deseja mesmo utilizar esses valores?"))
    {
        test1.style.display = 'none';
        btn_next.style.display = 'none';
        test2.style.display = 'flex';
        btn_change.style.display = 'block';
    }
}

function confirmValues() {
    if (confirm("Deseja mesmo utilizar esses valores?"))
    {
        form.submit()
    }
}