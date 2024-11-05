// wwwroot/js/workSchedule.js

let workDayIndex = 0; // Inicjalizuj indeks na 0

function initializeWorkDayIndex(count) {
    workDayIndex = count; // Ustaw indeks na wartość przekazaną z widoku
}

function addWorkDay() {
    const workDaysContainer = document.getElementById('workDaysContainer');
    const newWorkDay = `
        <div class="work-day">
            <h4>Work Day ${workDayIndex + 1}</h4>
            <div class="form-group">
                <label for="WorkDays[${workDayIndex}].WorkDate" class="control-label">Work Date</label>
                <input name="WorkDays[${workDayIndex}].WorkDate" class="form-control" type="date" />
                <span class="text-danger"></span>
            </div>
            <div class="form-group">
                <label for="WorkDays[${workDayIndex}].StartTime" class="control-label">Start Time</label>
                <input name="WorkDays[${workDayIndex}].StartTime" class="form-control" type="time" />
                <span class="text-danger"></span>
            </div>
            <div class="form-group">
                <label for="WorkDays[${workDayIndex}].EndTime" class="control-label">End Time</label>
                <input name="WorkDays[${workDayIndex}].EndTime" class="form-control" type="time" />
                <span class="text-danger"></span>
            </div>
        </div>`;
    workDaysContainer.insertAdjacentHTML('beforeend', newWorkDay);
    workDayIndex++;
}
