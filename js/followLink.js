var followLink ="";
        var collection
        function notLink(){
            followLink="";
        }
        function clear_follow(){
            collection = document.getElementsByTagName("div");
            for (var i=0; i<collection.length; i++) {
                    if (collection[i].className == "MultiBoxClose"){                    
                        collection[i].setAttribute('onClick', 'return notLink();');
                        document.getElementById('Overlay').setAttribute('onClick','notLink();');
                    }
            }
        }