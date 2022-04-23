var connection = new signalR.HubConnectionBuilder()
    .withUrl("/overlayHub")
    .withAutomaticReconnect()
    .build();

connection.on("UpdateAttempt", function (liftAttempt) {
    updateOverlay(liftAttempt);
});

connection.on("UpdateResult", function (liftResult) {
    updateResult(liftResult);
})

connection
    .start()
    .catch(function (err) {
        return console.error(err.toString());
    });

let replaceDataTimeout;
let showCentreOverlayTimeout;
let clearOverlaysTimeout;

function updateOverlay(liftAttempt) {
    // Clear any pending timeouts.
    clearTimeout(replaceDataTimeout);
    clearTimeout(showCentreOverlayTimeout);
    clearTimeout(clearOverlaysTimeout);

    const overlayCentreElem = document.getElementById("overlayCentre");
    const overlayLeftElem = document.getElementById("overlayLeft");
    const containerLiftResultElem = document.getElementById("containerLiftResult");

    // Ensure both overlays are cleared.
    overlayCentreElem.classList.remove("show");
    overlayLeftElem.classList.remove("show");

    //Ensure previous result is cleared so it doesn't show up for new lifter
    containerLiftResultElem.classList.remove("show");

    // Wait for 500ms before updating data, to ensure that overlays have been cleared from screen.
    replaceDataTimeout = setTimeout(
        function () {
            replaceLifterData(liftAttempt);
        },
        500);

    // First show the side overlay.
    overlayLeftElem.classList.add("show");

    // After 15 secs, replace with centre overlay.
    showCentreOverlayTimeout = setTimeout(
        function () {
            overlayLeftElem.classList.remove("show");
            overlayCentreElem.classList.add("show");
        },
        15000);

    // After 120 secs, clear all overlays.
    clearOverlaysTimeout = setTimeout(
        function () {
            overlayCentreElem.classList.remove("show");
            overlayLeftElem.classList.remove("show");
        },
        120000);
}

function replaceLifterData(liftAttempt) {
    //Update left overlay.
    const lifterNameElem = document.getElementById("lifterName");
    const currentTotalsElem = document.getElementById("SBDT");
    const statusElem = document.getElementById("status");
    const attemptWeightElem = document.getElementById("attemptWeight");
    const categoryElem = document.getElementById("category");
    const compNameElem = document.getElementById("compName");

    lifterNameElem.innerHTML = liftAttempt.lifter.name;
    let currentTotals = `S: ${liftAttempt.lifter.currentBestSquat}<br>B: ${liftAttempt.lifter.currentBestBench}<br>D: ${liftAttempt.lifter.currentBestDeadlift}<br>`
    if (liftAttempt.lifter.currentTotal > 0) currentTotals += `T: ${liftAttempt.lifter.currentTotal}`
    currentTotalsElem.innerHTML = currentTotals;
    statusElem.innerHTML = liftAttempt.lift;
    attemptWeightElem.innerHTML = liftAttempt.weight + " " + liftAttempt.weightUnit;
    categoryElem.innerHTML = liftAttempt.categoryName;
    compNameElem.innerHTML = liftAttempt.competitionName;

    // Update centre overlay.
    const lifterNameCentreElem = document.getElementById("lifterNameCentre");
    const attemptWeightCentreElem = document.getElementById("attemptWeightCentre");
    const instagramHandleElem = document.getElementById("instagramHandle");
    const containerInstaElem = document.getElementById("containerInsta");

    lifterNameCentreElem.innerHTML = liftAttempt.lifter.name;
    attemptWeightCentreElem.innerHTML = liftAttempt.weight + " " + liftAttempt.weightUnit;
    if (liftAttempt.lifter.instagram) {
        instagramHandleElem.innerHTML = `Instagram: @${liftAttempt.lifter.instagram}`;
        containerInstaElem.classList.add("show");
    }
    else {
        instagramHandleElem.innerHTML = "Instagram: ";
        containerInstaElem.classList.remove("show");
    }
}

function updateResult(liftResult) {
    const containerLiftResultElem = document.getElementById("containerLiftResult");
    const liftResultElem = document.getElementById("liftResult");

    containerLiftResultElem.classList.add("show");
    if (liftResult.liftSuccessful) {
        containerLiftResultElem.classList.remove("noLift");
        containerLiftResultElem.classList.add("goodLift");
        liftResultElem.innerHTML = "Good Lift";
    }
    else {
        containerLiftResultElem.classList.remove("goodLift");
        containerLiftResultElem.classList.add("noLift");
        liftResultElem.innerHTML = "No Lift";
    }
}