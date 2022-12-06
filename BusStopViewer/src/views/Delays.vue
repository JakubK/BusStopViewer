<template>
    <div>
        <h2>Delays for stop with id: {{ id }}</h2>
        <el-table-v2
            :columns="columns"
            :data="delays"
            :width="700"
            :height="400"
            fixed>
        </el-table-v2>
    </div>
</template>

<script lang="ts" setup>
import { Column } from 'element-plus';
import { onMounted, Ref, ref } from 'vue';
import { Delay } from '../models/delay';

import stopService from '../services/stops';

const props = defineProps({
    id: {
        type: String,
        required: true
    }
})

const delays: Ref<Delay[]> = ref([]);
onMounted(async() => {
    delays.value = await stopService.fetchStopDelays(+props.id);
});

const columns: Partial<Column<any[]>> = [
    {
        key: 'headsign',
        dataKey: 'headsign',
        title: 'HeadSign',
        width: 120
    },
    {
        key: 'theoreticalTime',
        dataKey: 'theoreticalTime',
        title: 'Theoretical Time',
        width: 120
    },
    {
        key: 'estimatedTime',
        dataKey: 'estimatedTime',
        title: 'Estimated Time',
        width: 120
    },
    {
        key: 'routeId',
        dataKey: 'routeId',
        title: 'Route Id',
        width: 120
    },
]
</script>