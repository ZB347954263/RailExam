// Javascript for TreeView most
// Copy node and it's subnodes
function treeNodeDeepCopy(node){
    //alert(node.get_id());
    var newNode = new ComponentArt.Web.UI.TreeViewNode();  
    
    newNode.set_text(node.get_text());
    newNode.set_value(node.get_value());
    newNode.set_id(node.get_id());
    newNode.set_toolTip(node.get_toolTip());
    newNode.set_imageUrl(node.get_imageUrl());
    newNode.set_navigateUrl(node.get_navigateUrl());
    
    nodes = node.get_nodes();
    if(nodes.get_length() == 0){
        return newNode;    
    }
    
    for(var i=0; i<nodes.get_length(); i++){
        //nodes.getNode(i).get_parentTreeView().set_autoAssignNodeIDs(false);
        newNode.get_nodes().add(treeNodeDeepCopy(nodes.getNode(i)));
    }
    
    return newNode;
}
function treeNodeDeepCopy(node){
    //alert(node.get_id());
    var newNode = new ComponentArt.Web.UI.TreeViewNode();  
    
    newNode.set_text(node.get_text());
    newNode.set_value(node.get_value());
    newNode.set_id(node.get_id());
    newNode.set_toolTip(node.get_toolTip());
    newNode.set_imageUrl(node.get_imageUrl());
    newNode.set_navigateUrl(node.get_navigateUrl());
    
    nodes = node.get_nodes();
    if(nodes.get_length() == 0){
        return newNode;    
    }
    
    for(var i=0; i<nodes.get_length(); i++){
        //nodes.getNode(i).get_parentTreeView().set_autoAssignNodeIDs(false);
        newNode.get_nodes().add(treeNodeDeepCopy(nodes.getNode(i)));
    }
    
    return newNode;
}
function copy(node){
    var newNode = new ComponentArt.Web.UI.TreeViewNode();  
    
    newNode.set_text(node.get_text());
    newNode.set_value(node.get_value());
    newNode.set_id(node.get_id());
    newNode.set_toolTip(node.get_toolTip());
    newNode.set_imageUrl(node.get_imageUrl());
    newNode.set_navigateUrl(node.get_navigateUrl());

    return newNode;
}

function exchange(node1, node2){
    var temp = new ComponentArt.Web.UI.TreeViewNode();  
    
    if(node2.get_nodes().get_length() == 0){
        temp = copy(node2);
        node1.get_parentNode().get_nodes().insert(temp, node1.get_index());
        alert(temp.get_parentTreeView());
        temp = node1.get_parentNode().get_nodes().getNode(node1.get_index());
    }
    else{
        for(var i=0; i<node2.get_nodes().get_length(); i++){
            alert(temp.get_parentTreeView());
            temp.get_nodes().add(copy(node2.get_nodes().getNode(i)))
        }
    }
}        
