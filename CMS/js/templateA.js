$(function() {
    $("#tabs").tabs();
    var linkTitle1 =  document.getElementById("txtLinkTitle1").value;
    var linkTitle2 = document.getElementById("txtLinkTitle2").value;
    var link1 = document.getElementById("txtLink1").value;
    var link2 = document.getElementById("txtLink2").value;
    if(linkTitle1 != "" || linkTitle2!="" || link1!="" || link2!=""){
        document.getElementById("tabs").style.display = "block";
        document.getElementsByName("optionalLinks")[0].checked = "true";
    }
});

function displayOptionalLinks(value){
    var myvalue = value;
    if(myvalue=="yes"){
        document.getElementById('tabs').style.display = 'block';
    }else{
        document.getElementById('tabs').style.display = 'none';
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

function getInternalPagelink(link)
{
    if(document.getElementById('LinkID').value == 1)
    {
        document.getElementById('txtLink1').value= link;
    }else{
        document.getElementById('txtLink2').value= link;
    }
    
    if(document.getElementById('LinkIDmore').value == 1)
    {
        document.getElementById('txtLinkmore').value= link;
    }else{
        document.getElementById('txtLinkmore2').value= link;
    }
}

function is_landing_page(txt){
    var txt_url_landing = txt;
    var regex = /^[a-zA-Z0-9_.?]{0,}(LandingId=){1}[a-zA-Z0-9_.&]{0,}$/;
    if (regex.test(txt_url_landing)){
        document.getElementById("p_new_landing").style.display="none";
    }else{
        document.getElementById("p_edit_landing").style.display="none";
    }
}

function radioExternal2_onclick(linkId_number) {
    document.getElementById("radioType2").value = 1;
    document.getElementById("LinkID").value = linkId_number;
    document.getElementById("div_internalLink"+linkId_number).style.display="none";
    document.getElementById("htmlElement"+linkId_number).style.display = "none";
    document.getElementById("internal_links_container").style.display="none";
}

function radioInternal2_onclick(linkId_number) {
    document.getElementById("radioType2").value = 2;
    document.getElementById("LinkID").value = linkId_number;
    document.getElementById("div_internalLink"+linkId_number).style.display="block";
    document.getElementById("internal_links_container").style.display="block";
    document.getElementById("htmlElement"+linkId_number).style.display = "none";
    $("#internal_links_container").appendTo("#div_internalLink"+linkId_number);
}

function radioNewLanding2_onclick(linkId_number) {
    document.getElementById("radioType2").value = 3;
    document.getElementById("LinkID").value = linkId_number;
    document.getElementById("div_internalLink"+linkId_number).style.display="none";
    document.getElementById("htmlElement"+linkId_number).style.display = "block";
    document.getElementById("internal_links_container").style.display="none";
}