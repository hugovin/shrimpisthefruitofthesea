        function downloadTrial(resourceId){
            //alert("Voy a descargar!");
            searchAjax = new nuevoAjax();
            searchAjax.open("GET","getResourceTrialLink.aspx?resourceId="+resourceId+"&"+ (new Date()).getTime(),true);
            searchAjax.onreadystatechange=function() {
                if (searchAjax.readyState==4) {
                        result = searchAjax.responseText;
                        location.href=result;
                }
            }
            searchAjax.send(null);
        }