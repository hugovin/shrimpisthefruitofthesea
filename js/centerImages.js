// JavaScript Document

// Funcion para Centrar Imagenes en un div con medidas especificas
// Ivan Piedra
//	Estructura
//	<div id="boxContImage1" style="width:300px; height:540px;">
//		<img id="images1" src="images/Sunset.jpg" onload="getDim(document.getElementById('boxContImage1'),this)"/>
//	</div>

//Lee los tamaños del <div id="boxContImage"/> y ajusta la imagen <img id="images1"/> proporcionalmente al div
//Se necesita indicar el nombre del div contenedor en el onload y si en una pagina hay mas de un llamado a la funcion
//los nombres del los divs tiene que ser diferente

//Ejemplo: funcionImages2.html


function getDim(box,images){
                                
                var boxWidth = parseInt(box.style.width);
                var boxHeight = parseInt(box.style.height);
                
                myImage = new Image();
                myImage = images;
                
                var minW = myImage.width;
                var minH = myImage.height;
                
                var r = myImage.height/minW;

                if((minW>boxWidth) || (minH>boxHeight)){      
                                if (myImage.width>boxWidth) {
                                                myImage.width = boxWidth;
                                                myImage.height = Math.floor(myImage.width*r);           
                                }
                                if (myImage.height>boxHeight) {
                                                myImage.height = boxHeight;
                                                myImage.width = Math.floor(myImage.height/r);            
                                }
                }else{
                                if (myImage.width>=myImage.height) {
                                                
                                                myImage.width=boxWidth;
                                                myImage.height=Math.floor(boxWidth * minH / minW);

                                                /*myImage.width = boxWidth;
                                                myImage.height = Math.round(myImage.width*r);        */
                                }else{
                                                
                                                myImage.height=boxHeight;
                                                myImage.width=Math.floor(boxHeight * minW / minH);


                                                /*myImage.height = boxHeight;
                                                myImage.width = Math.round(myImage.height/r);         */
                                }              
                }
                                                
                myImage.style.marginTop = ((boxHeight - myImage.height)/2) + "px";
                myImage.style.marginLeft = ((boxWidth - myImage.width)/2) + "px";

                myImage.style.marginTop = myImage.style.marginTop;
                myImage.style.marginLeft = myImage.style.marginLeft;
}