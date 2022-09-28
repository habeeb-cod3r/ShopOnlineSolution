function MakeUpdateQtyButtonVisible(id, visible)
{
    const UpdateQtyButton = document.querySelector("button[data-itemId='" + id + "']");
    if (visible == true) {
        UpdateQtyButton.style.display = "inline-block";
    }
    else {
        UpdateQtyButton.style.display = "none";
    }
}