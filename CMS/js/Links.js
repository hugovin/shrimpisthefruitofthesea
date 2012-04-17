function enable_reference_image(my_value, id_container_uploader, id_container_url){
    var value_test=my_value;
    var my_container_uploader = id_container_uploader;
    var my_container_url = id_container_url;
    if(value_test=="url"){
        document.getElementById(my_container_uploader).style.display = 'none';
        document.getElementById(my_container_url).style.display = 'block';
    }
    if(value_test=="upload"){
        document.getElementById(my_container_url).style.display = 'none';
        document.getElementById(my_container_uploader).style.display = 'block';
    }
}

function radioExternal_onclick(id) {
    document.getElementById('radiotipo').value = "";
    document.getElementById('LinkID').value = 1;
    document.getElementById('radiotipo').value = 1;
    document.getElementById(id).style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
    document.getElementById('div_internalLink').style.visibility="hidden" ;
}

function radioInternal_onclick(id) {
    document.getElementById('radiotipo').value = "";
    document.getElementById('radiotipo').value = 2;
    document.getElementById('LinkID').value = 1;
    document.getElementById(id).style.visibility="visible" ;
    document.getElementById('txt_link').style.visibility="visible" ;
        document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
}

function getInternalPagelink(link)
{

    if(document.getElementById('LinkID').value == 1)
    {
        document.getElementById('txt_link').value= link;
    }else{
        document.getElementById('txt_link2').value= link;
    }
    try{
    if(document.getElementById('LinkIDmore').value == 1)
    {
        document.getElementById('txtLinkmore').value= link;
    }else{
        document.getElementById('txtLinkmore2').value= link;
    }
    }catch(err){}   
}

function radioExternal2_onclick(id) {
    document.getElementById('radiotipo2').value = "";
    document.getElementById('radiotipo2').value = 1;
    document.getElementById('LinkID').value = 2;
        document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
}

function radioInternal2_onclick(id) {
    document.getElementById('radiotipo2').value = "";
    document.getElementById('LinkID').value = 2;
    document.getElementById('radiotipo2').value = 2;
    document.getElementById(id).style.visibility="visible" ;
        document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
}


//------------------------------------------------------------------------------
function radioExternalmore_onclick(id) {
    document.getElementById('radiotipomore').value = "";
    document.getElementById('LinkIDmore').value = 1;
    document.getElementById('radiotipomore').value = 1;
    document.getElementById(id).style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
    document.getElementById('div_internalLink').style.visibility="hidden" ;
}

function radioInternalmore_onclick(id) {
    document.getElementById('radiotipomore').value = "";
    document.getElementById('radiotipomore').value = 2;
    document.getElementById('LinkIDmore').value = 1;
    document.getElementById(id).style.visibility="visible" ;
    document.getElementById('txtLinkmore').style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
}
//-----------------------------------------------------------------------
function radioNewLanding_onclick() {
    document.getElementById('radiotipo').value = 3;
    document.getElementById('LinkID').value = 1;
    document.getElementById('LinkTypeT').value = 1;    
    document.getElementById('txt_link').style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'visible';
    document.getElementById('div_landingPage').style.visibility="visible" ;   
    document.getElementById('div_internalLink').style.visibility="hidden" ;
    document.getElementById('txtlandTitle').value = document.getElementById('landingPageTitle1').value;
    tinyMCE.activeEditor.setContent(document.getElementById('landingPageContent1').value);
    document.getElementById('imageUrlLanding').value = document.getElementById('landingPageImage1').value;
}

function radioNewLanding2_onclick() {
    document.getElementById('radiotipo2').value = 3;
    document.getElementById('LinkID').value = 2;
    document.getElementById('LinkTypeT').value = 2;
    document.getElementById('txt_link2').style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'visible';
    document.getElementById('div_landingPage').style.visibility="visible" ;   
    document.getElementById('div_internalLink').style.visibility="hidden" ;
    document.getElementById('txtlandTitle').value = document.getElementById('landingPageTitle2').value;
    tinyMCE.activeEditor.setContent(document.getElementById('landingPageContent2').value);
    document.getElementById('imageUrlLanding').value = document.getElementById('landingPageImage2').value;
}

function radioNewLandingmore_onclick() {
    document.getElementById('radiotipomore').value = 3;
    document.getElementById('LinkIDmore').value = 1;
    document.getElementById('LinkTypeT').value = 2;
    document.getElementById('txtLinkmore').style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'visible';
    document.getElementById('div_landingPage').style.visibility="visible" ;   
    document.getElementById('div_internalLink').style.visibility="hidden" ;
    document.getElementById('txtlandTitle').value = document.getElementById('landingPageTitle3').value;
    tinyMCE.activeEditor.setContent(document.getElementById('landingPageContent3').value);
    document.getElementById('imageUrlLanding').value = document.getElementById('landingPageImage3').value;
}

function radioNewLandingmore2_onclick() {
    document.getElementById('radiotipomore2').value = 3;
    document.getElementById('LinkIDmore').value = 2;
    document.getElementById('LinkTypeT').value = 2;
    document.getElementById('txtLinkmore2').style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'visible';
    document.getElementById('div_landingPage').style.visibility="visible" ;   
    document.getElementById('div_internalLink').style.visibility="hidden" ; 
    document.getElementById('txtlandTitle').value = document.getElementById('landingPageTitle4').value;
    tinyMCE.activeEditor.setContent(document.getElementById('landingPageContent4').value);
    document.getElementById('imageUrlLanding').value = document.getElementById('landingPageImage4').value;
}

//-----------------------------------------------------------------------
function radioExternalmore2_onclick(id) {
    document.getElementById('radiotipomore2').value = "";
    document.getElementById('LinkIDmore').value = 2;
    document.getElementById('radiotipomore2').value = 1;
    document.getElementById(id).style.visibility="visible" ;
    document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
    document.getElementById('div_internalLink').style.visibility="hidden" ;
}

function radioInternalmore2_onclick(id) {
    document.getElementById('radiotipomore2').value = "";
    document.getElementById('radiotipomore2').value = 2;
    document.getElementById('LinkIDmore').value = 2;
    document.getElementById(id).style.visibility="visible" ;
    document.getElementById('txtLinkmore2').style.visibility="visible" ;
        document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
}