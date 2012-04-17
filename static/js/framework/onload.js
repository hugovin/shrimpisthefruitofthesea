// Multiple Onload Event Processor
// (c) Jordan Sherer 2007

if(window.ie || (Browser != undefined && Browser.Engine.trident))
{
    window.onload = doOnLoad;
}
else
{
    window.addEvent("domready", doOnLoad);
}

var loadEvents = [];

// Use this function to add a function to the end of the queue that will be called on DOMReady
function addOnLoad(func)
{
    id = loadEvents.length;
    loadEvents[id] = func;
    return id;
}

// Use this function to insert a function at position index that will be called on DOMReady
function insertOnLoad(func, index)
{
    lastIndex = loadEvents.length - 1;
    if(index > lastIndex){ index = lastIndex; }
    loadEvents.splice(index, 0, func);
}

// Use this function to delete a function a position id
function deleteOnLoad(id)
{
    loadEvents.splice(id,1);
}

function replaceOnLoad(id, func)
{
    loadEvents[id] = func;
}

function getLoadEvents()
{
    return loadEvents;
}

// Actually run the events
function doOnLoad(e)
{
    count = loadEvents.length;
    for(i=0; i<count; i++)
    {
        func = loadEvents[i];
        if(func != undefined)
        {
            func();
        }
    }
}
