<template>
    <div>
        <h2>All stops</h2>
        <el-table 
            :stripe="true"
            :border="true"
            :data="filteredTableData" style="width: 100%">
            <el-table-column prop="stopId" label="Stop Id" width="180" />
            <el-table-column prop="stopName" label="Stop Name" width="180" />
            <el-table-column prop="subName" label="Stop SubName" />
            <el-table-column :align="'right'">
                <template #header>
                    <el-input v-model="search" size="small" placeholder="Type to search" />
                </template>
                <template #default="scope">
                    <el-button size="small" @click="handleAssign(scope.row)">
                        Assign
                    </el-button>
                </template>
            </el-table-column>
        </el-table>
    </div>
</template>
<script lang="ts" setup>
import { computed, onMounted, ref, Ref } from 'vue';
import { Stop } from '../models/stop';
import stopService from '../services/stops';

const search = ref('');
const tableData: Ref<Stop[]> = ref([]);
onMounted(async() => {
    tableData.value = await stopService.fetchAllStops();
})

const filteredTableData = computed(() => {
    if(search.value.length === 0)
        return tableData.value;
    return tableData.value.filter(x => {
        return x.stopName?.toLowerCase().includes(search.value.toLowerCase());
    });
});

const handleAssign = async(row: Stop) => {
    await stopService.assignStopToUser(row.stopId);
}

const handleRemove = async(row: Stop) => {
    await stopService.removeStopFromUser(row.stopId);
}

</script>