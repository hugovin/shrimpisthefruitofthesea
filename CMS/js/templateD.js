$(document).ready(function(){
    $('#div_internalLink').children().children().children('img').css('display','none');
    if(document.getElementById("radiotipo")!=null){
        var linkType = document.getElementById("radiotipo").value;
        if(linkType == "1"){
            document.getElementById('radioExternal').checked = 'checked';
        }
        if(linkType == "2"){
            document.getElementById('radioInternal').checked = 'checked';
        }
        if(linkType == "3"){
            document.getElementById('radioNewLanding').checked = 'checked';
            document.getElementById("htmlElement").style.display="block";
            document.getElementById("txt_link").readOnly=true;
        }
    }
});

function is_landing_page(txt){
    var txt_url_landing = txt;
    var regex = /^[a-zA-Z0-9_.?]{0,}(LandingId=){1}[a-zA-Z0-9_.&]{0,}$/;
    if (regex.test(txt_url_landing)){
        document.getElementById("p_new_landing").style.display="none";
    }else{
        document.getElementById("p_edit_landing").style.display="none";
    }
}

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

function radioExternal_onclick(e) {
    document.getElementById('radiotipo').value = "";
    document.getElementById('LinkID').value = 1;
    document.getElementById('radiotipo').value = 1;
    document.getElementById("div_internalLink").style.display="none" ;
    document.getElementById("htmlElement").style.display="none" ;
    document.getElementById("txt_link").readOnly=false;
    return;
}

function radioInternal_onclick(e) {
    document.getElementById('radiotipo').value = "";
    document.getElementById('radiotipo').value = 2;
    document.getElementById('LinkID').value = 1;
    document.getElementById("div_internalLink").style.display="block" ;
    document.getElementById("htmlElement").style.display="none" ;
    document.getElementById("txt_link").readOnly=true;
    document.getElementById("txt_link").value="";
    return;
}

function radioNewLanding_onclick(e) {
    document.getElementById('radiotipo').value = 3;
    document.getElementById('LinkID').value = 1;    
    document.getElementById("div_internalLink").style.display="none";
    document.getElementById("htmlElement").style.display="block";
    document.getElementById("txt_link").readOnly=true;
    return;
}

function getInternalPagelink(link)
{
    document.getElementById('txt_link').value= link;
}

function radioExternal2_onclick(id, linkId_number) {
    document.getElementById('radiotipo2').value = 1;
    document.getElementById('LinkID').value = linkId_number;
    document.getElementById('div_internalLink').style.display="none" ;
    document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
}

function radioInternal2_onclick(linkId_number) {
    document.getElementById('radiotipo2').value = 2;
    document.getElementById('LinkID').value = linkId_number;
    document.getElementById('div_internalLink').style.display="block" ;
    document.getElementById('htmlElement').style.visibility = 'hidden';
    document.getElementById('div_landingPage').style.visibility="hidden" ;
}

function radioNewLanding2_onclick(linkId_number) {
    document.getElementById('radiotipo2').value = 3;
    document.getElementById('LinkID').value = linkId_number;
    document.getElementById('div_internalLink').style.display="none" ;
    document.getElementById('htmlElement').style.visibility = 'visible';
    document.getElementById('div_landingPage').style.visibility="visible" ;   
}