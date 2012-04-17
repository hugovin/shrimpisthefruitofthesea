//Erase the AjaxObject to move it to other javascript file and use it in other site pages.
   
    function getSug(e){
        tecla = document.all ? e.keyCode : e.which; 
        //if(tecla==13) alert('Gracias por pulsarme'); 
        var espacio = document.getElementById('itemList');
        var word = document.getElementById('topSearch').value;
        //If is enter we can send to the search page
        if(tecla == 13 && word != ""){ TopSearch(); }
            if(textComeFrom == ""){
               // espacio.innerHTML = '<center><img src="images/loading.gif" border="0" width="30"/></center>';
            }
            if((word!="")){
                if(word != textComeFrom){
                        /*textComeFrom = word;
                        //espacio.innerHTML = '';
                        espacio.style.border="1px solid black";
                        espacio.style.backgroundColor="white";      
                        searchAjax = new nuevoAjax();
                        searchAjax.open("GET","dropdownlist.aspx?word="+word,true);
                        searchAjax.onreadystatechange=function(){
                            if (searchAjax.readyState==4) {
                                    espacio.innerHTML=searchAjax.responseText;
                            }
                        }
                        //alert("Send request!");
                        // onkeyup="getSug();"*/
                        //searchAjax.send(null);
                }
            }else{
                cleanSug();
            }
    }
    function cleanSug(){
        var espacio = document.getElementById('itemList');
        espacio.innerHTML = '';
        espacio.style.border='';
        espacio.style.background='none';
        textComeFrom='';
    }
    function readKey(e){
        alert("Presiono");
    }