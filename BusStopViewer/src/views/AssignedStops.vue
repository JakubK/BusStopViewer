<template>
    <div>
        <h2>My stops</h2>
        <Searchbar v-model="search" placeholder="Type to search"></Searchbar>
        <el-table-v2
            :columns="columns"
            :data="filteredTableData"
            :width="700"
            :height="400"
            fixed>
        </el-table-v2>
    </div>
</template>

<script lang="tsx" setup>
import { computed, onMounted, ref, Ref } from 'vue';
import { Stop } from '../models/stop';
import stopService from '../services/stops';
import type { Column } from 'element-plus'
import router from '../router';
import Searchbar from '../components/Searchbar.vue';

const columns: Partial<Column<any[]>> = [
    {
        key: 'stopId',
        dataKey: 'stopId',
        title: 'Stop Id',
        width: 120
    },
    {
        key: 'stopName',
        dataKey: 'stopName',
        title: 'Stop name',
        width: 120
    },
    {
        key: 'subName',
        dataKey: 'subName',
        title: 'Sub name',
        width: 120
    },
    {
        key: 'actions',
        title: 'Actions',
        width: 120,
        cellRenderer: ({ rowData }) => (
            <div>
                <el-button onclick={() => handleViewDelays(rowData.stopId)}>View delays</el-button>
                <el-button type="danger" onclick={() => handleRemove(rowData.stopId)}>Remove</el-button>
            </div>
        ),
    },
]

const search = ref('');
const tableData: Ref<Stop[]> = ref([]);
onMounted(async() => {
    tableData.value = await stopService.fetchOwnedStops();
})

const filteredTableData = computed(() => {
    if(search.value.length === 0)
        return tableData.value;
    return tableData.value.filter(x => {
        return x.stopName?.toLowerCase().includes(search.value.toLowerCase());
    });
});

const handleRemove = async(stopId: number) => {
    await stopService.removeStopFromUser(stopId);
}

const handleViewDelays = (stopId: number) => {
    router.push(`/stops/${stopId}/delays`);
}

</script>